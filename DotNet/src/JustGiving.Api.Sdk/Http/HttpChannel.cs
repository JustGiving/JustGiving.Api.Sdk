using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Http;

namespace JustGiving.Api.Sdk.Http
{
    public class HttpChannel
    {
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IHttpClient _httpClient;

        public HttpChannel(ClientConfiguration clientConfiguration, IHttpClient httpClient)
        {
            if(httpClient == null)
            {
                throw new ArgumentNullException("httpClient", "httpClient must not be null to access the api.");
            }

            _clientConfiguration = clientConfiguration;
            _httpClient = httpClient;

            if (!string.IsNullOrEmpty(_clientConfiguration.Username))
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientConfiguration.Username + ":" + _clientConfiguration.Password));
                _httpClient.AddHeader("Authorization", "Basic " + credentials);
            }
        }

        public HttpResponseMessage PerformRawRequest(string method, string locationFormat)
        {
            var url = BuildUrl(locationFormat);
            var request = new HttpRequestMessage(method, url);
            return _httpClient.Send(request);
        }

        public HttpResponseMessage PerformRawRequest(string method, string locationFormat, string contentType, byte[] postData)
        {
            var url = BuildUrl(locationFormat);
            var content = HttpContent.Create(postData, contentType);
            var request = new HttpRequestMessage(method, url, content);
            return _httpClient.Send(request);
        }

        public TResponseType PerformApiRequest<TResponseType>(string method, string locationFormat)
        {
            return PerformApiRequest<object, TResponseType>(method, locationFormat, null);
        }

        public TResponseType PerformApiRequest<TRequestType, TResponseType>(string method, string locationFormat, TRequestType requestObject) where TRequestType : class
        {
            var url = BuildUrl(locationFormat);
            HttpRequestMessage request;
            if (requestObject != null)
            {
                var payload = BuildPayload(requestObject);
                request = new HttpRequestMessage(method, url, payload);
            }
            else
            {
                request = new HttpRequestMessage(method, url);
            }

            var response = _httpClient.Send(request);

            var responseContent = response.Content.ReadAsString();
            ThrowExceptionForExceptionalStatusCodes(response, responseContent);
            
            return DeserializeContentFromXml<TResponseType>(responseContent);
        }

        private static void ThrowExceptionForExceptionalStatusCodes(HttpResponseMessage response, string content)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Continue:
                case HttpStatusCode.Found:
                    var errorsDespiteSuccess = TryExtractErrorsFromResponse(content);
                    if (errorsDespiteSuccess != null)
                    {
                        throw ErrorResponseExceptionFactory.CreateException(response, content, errorsDespiteSuccess);
                    }
                    return;
                default:
                    var errors = TryExtractErrorsFromResponse(content);
                    throw ErrorResponseExceptionFactory.CreateException(response, content, errors);
            }
        }

        private static Errors TryExtractErrorsFromResponse(string rawResponse)
        {
            try
            {
                return DeserializeContentFromXml<Errors>(rawResponse);
            }
            catch
            {
                return null;
            }
        }

        private Uri BuildUrl(string locationFormat)
        {
            var location = string.Format(locationFormat, _clientConfiguration.ApiKey, _clientConfiguration.ApiVersion);
            location += !location.Contains("?") ? "?" : "&" + "debug=true";

            return new Uri(location);
        }

        private static HttpContent BuildPayload<TPayloadType>(TPayloadType objectToSerialise)
        {
            var payloadContent = SerializeContentToXml(objectToSerialise);
            var payload = HttpContent.Create(payloadContent, "application/xml");
            return payload;
        }

        private static string SerializeContentToXml<TPayloadType>(TPayloadType objectToSerialise)
        {
            using (var memoryStream = new MemoryStream())
            {
                var dataContractSerializer = new DataContractSerializer(objectToSerialise.GetType());
                dataContractSerializer.WriteObject(memoryStream, objectToSerialise);
                memoryStream.Flush();
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static TResponseType DeserializeContentFromXml<TResponseType>(string content)
        {
            if(string.IsNullOrEmpty(content))
            {
                throw new ApiClientException("An attempt was made to deserialize a response which was empty.");
            }

            try
            {
                var reader = new DataContractSerializer(typeof (TResponseType));
                var byteArray = Encoding.ASCII.GetBytes(content);
                var stream = new MemoryStream(byteArray);
                return (TResponseType) reader.ReadObject(stream);
            }
            catch(Exception ex)
            {
                var exception = new ApiClientException("An error occured while deserializing the incoming response", ex);
                ex.Data.Add("RawContent", content);
                throw exception;
            }
        }
    }
}