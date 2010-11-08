using System;
using System.Net;

namespace JustGiving.Api.Sdk.Http
{
    public class HttpChannel
    {
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IHttpClient _httpClient;
        private readonly MultiformatPayloadBuilder _payloadBuilder;

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
            _payloadBuilder = new MultiformatPayloadBuilder(_clientConfiguration);

            SetAuthenticationHeaders();
        }

        private void SetAuthenticationHeaders()
        {
            if (!string.IsNullOrEmpty(_clientConfiguration.Username) && !string.IsNullOrEmpty(_clientConfiguration.Password))
            {
                var credentials = new HttpBasicAuthCredentials(_clientConfiguration.Username, _clientConfiguration.Password);
                _httpClient.AddHeader("Authorize", "Basic " + credentials);
                _httpClient.AddHeader("Authorization", "Basic " + credentials);
            }
        }

        public DataPackets.HttpResponseMessage PerformRawRequest(string method, string locationFormat)
        {
            var url = BuildUrl(locationFormat);
            var request = new DataPackets.HttpRequestMessage(method, url);
            return _httpClient.Send(request);
        }

        public DataPackets.HttpResponseMessage PerformRawRequest(string method, string locationFormat, string contentType, byte[] postData)
        {
            var url = BuildUrl(locationFormat);
            return _httpClient.Send(method, url, postData, contentType);
        }

        public TResponseType PerformApiRequest<TResponseType>(string method, string locationFormat)
        {
            return PerformApiRequest<object, TResponseType>(method, locationFormat, null);
        }

        public TResponseType PerformApiRequest<TRequestType, TResponseType>(string method, string locationFormat, TRequestType request) where TRequestType : class
        {
            if(method.NotAccepted())
            {
                throw new ArgumentException("Invalid Http Method - Currently Supported Methods are GET, POST, PUT and HEAD", "method");
            }

            var url = BuildUrl(locationFormat);

            var payload = _payloadBuilder.BuildPayload(request);
            var response = _httpClient.Send(method, url, payload);
            var responseContent = ValidateResponse(response);

            return _payloadBuilder.UnpackResponse<TResponseType>(responseContent);
        }



        private string ValidateResponse(DataPackets.HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsString();
            ThrowExceptionForExceptionalStatusCodes(response, responseContent);
            
            if (string.IsNullOrEmpty(responseContent))
            {
                throw new ApiClientException("An attempt was made to deserialize an empty response.", response);
            }

            return responseContent;
        }

        private void ThrowExceptionForExceptionalStatusCodes(DataPackets.HttpResponseMessage response, string content)
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
                case HttpStatusCode.NotFound:
                    throw new ResourceNotFoundException();
                default:
                    var errors = TryExtractErrorsFromResponse(content);
                    throw ErrorResponseExceptionFactory.CreateException(response, content, errors);
            }
        }

        public Errors TryExtractErrorsFromResponse(string rawResponse)
        {
            try
            {
                return _payloadBuilder.UnpackResponse<Errors>(rawResponse);
            }
            catch
            {
                return null;
            }
        }

        private Uri BuildUrl(string locationFormat)
        {
            if (!locationFormat.Contains("{apiKey}") || !locationFormat.Contains("{apiVersion}"))
            {
                throw new ArgumentException("'locationFormat must contain '{apiKey}' and '{apiVersion}' placeholders (case sensitive).", "locationFormat");
            }

            var location =  locationFormat
                .Replace("{apiKey}", _clientConfiguration.ApiKey)
                .Replace("{apiVersion}", _clientConfiguration.ApiVersion.ToString());

            return new Uri(location);
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