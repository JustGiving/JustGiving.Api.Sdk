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
        public bool EnableDebug { get; set; }

        private readonly ClientConfiguration _clientConfiguration;
        private readonly IHttpClient _httpClient;
        
        public HttpChannel(ClientConfiguration clientConfiguration, IHttpClient httpClient)
        {
            if (clientConfiguration == null)
            {
                throw new ArgumentNullException("clientConfiguration", "clientConfiguration must not be null to access the api.");
            }

            if(httpClient == null)
            {
                throw new ArgumentNullException("httpClient", "httpClient must not be null to access the api.");
            }

            _clientConfiguration = clientConfiguration;
            _httpClient = httpClient;

            SetAuthenticationHeaders();
        }

        private void SetAuthenticationHeaders()
        {
            if (!string.IsNullOrEmpty(_clientConfiguration.Username) && !string.IsNullOrEmpty(_clientConfiguration.Password))
            {
                var credentials = new HttpBasicAuthCredentials(_clientConfiguration.Username, _clientConfiguration.Password);
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

        public TResponseType PerformApiRequest<TRequestType, TResponseType>(string method, string locationFormat, TRequestType request) where TRequestType : class
        {
            if(method.NotAccepted())
            {
                throw new ArgumentException(
                    "Invalid Http Method - Currently Supported Methods are GET, POST, PUT and HEAD", "method");
            }

            var url = BuildUrl(locationFormat);
            HttpRequestMessage httpRequestMessage;
            if (request != null)
            {
                var payload = BuildPayload(request);
                httpRequestMessage = new HttpRequestMessage(method, url, payload);
            }
            else
            {
                var content = HttpContent.Create(string.Empty, "application/xml");
                httpRequestMessage = new HttpRequestMessage(method, url, content);
            }

            var response = _httpClient.Send(httpRequestMessage);
            string responseContent = ValidateResponse(response);
            return DeserializeContentFromXml<TResponseType>(responseContent);
        }

        private static string ValidateResponse(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsString();
            ThrowExceptionForExceptionalStatusCodes(response, responseContent);
            
            if (string.IsNullOrEmpty(responseContent))
            {
                throw new ApiClientException("An attempt was made to deserialize an empty response.", response);
            }

            return responseContent;
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
                    if (errorsDespiteSuccess != null && errorsDespiteSuccess.Count > 0)
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
            if (!locationFormat.Contains("{apiKey}") || !locationFormat.Contains("{apiVersion}"))
                throw new ArgumentException(
                    "'locationFormat must contain '{apiKey}' and '{apiVersion}' placeholders (case sensitive).", "locationFormat");

            var location =  locationFormat
                .Replace("{apiKey}", _clientConfiguration.ApiKey)
                .Replace("{apiVersion}", _clientConfiguration.ApiVersion.ToString());
            
            if (EnableDebug)
            {
                location += !location.Contains("?") ? "?" : "&" + "debug=true";
            }

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

    static class Extensions
    {
        public static bool NotAccepted(this string method)
        {
            var lc = method.ToLower();
            return !(lc == "get" || lc == "post" || lc == "put" || lc == "head");
        }
    }
}