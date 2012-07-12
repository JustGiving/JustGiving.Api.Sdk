using System;
using System.Net;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk.Http
{
    public class HttpChannel
    {
    	public ClientConfiguration ClientConfiguration { get; private set; }
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

            ClientConfiguration = clientConfiguration;
            _httpClient = httpClient;
            _payloadBuilder = new MultiformatPayloadBuilder(ClientConfiguration);

            SetAuthenticationHeaders();
        }

        private void SetAuthenticationHeaders()
        {
            if (!string.IsNullOrEmpty(ClientConfiguration.Username) && !string.IsNullOrEmpty(ClientConfiguration.Password))
            {
                var credentials = new HttpBasicAuthCredentials(ClientConfiguration.Username, ClientConfiguration.Password);
                _httpClient.AddHeader("Authorize", "Basic " + credentials);
                _httpClient.AddHeader("Authorization", "Basic " + credentials);
            }
        }

		protected internal T Get<T>(string location)
		{
			return PerformRequest<T>("GET", location);
		}

		protected internal void GetAsync<T>(string location, Action<T> callback)
		{
			PerformRequestAsync("GET", location, callback);
		}

		protected internal TResponseType Put<TRequestType, TResponseType>(string location, TRequestType request) where TRequestType : class
		{
			if (request == null)
			{
				throw new ArgumentNullException("request", "Request cannot be null");
			}

			return PerformRequest<TRequestType, TResponseType>("PUT", location, request);
		}

		protected internal void PostAsync<TRequestType, TResponseType>(string location, TRequestType request, Action<TResponseType> callback) where TRequestType : class
		{
			PerformRequestAsync("POST", location, request, callback);
		}

		protected internal TResponseType Post<TRequestType, TResponseType>(string location, TRequestType request) where TRequestType : class
		{
			if (request == null)
			{
				throw new ArgumentNullException("request", "Request cannot be null");
			}

			return PerformRequest<TRequestType, TResponseType>("POST", location, request);
		}

		protected internal void PutAsync<TRequestType, TResponseType>(string location, TRequestType request, Action<TResponseType> callback) where TRequestType : class
		{
			PerformRequestAsync("PUT", location, request, callback);
		}

        public HttpResponseMessage PerformRawRequest(string method, string locationFormat)
        {
            var url = BuildUrl(locationFormat);
            var request = new HttpRequestMessage(method, url);
            return _httpClient.Send(request);
        }

        public HttpResponseMessage PerformRawRequest(string method, string locationFormat, string contentType)
        {
            var url = BuildUrl(locationFormat);
            var content = new HttpContent(contentType);
            return _httpClient.Send(method, url, content);
        }

        public HttpResponseMessage PerformRawRequest(string method, string locationFormat, string contentType, byte[] postData)
        {
            var url = BuildUrl(locationFormat);
            return _httpClient.Send(method, url, postData, contentType);
        }

        public void PerformRawRequestAsync(string method, string locationFormat, Action<HttpResponseMessage> httpClientCallback)
        {
            var url = BuildUrl(locationFormat);
            var request = new HttpRequestMessage(method, url);
            _httpClient.SendAsync(request, httpClientCallback);
        }

        public void PerformRawRequestAsync(string method, string locationFormat, string contentType, byte[] postData, Action<HttpResponseMessage> httpClientCallback)
        {
            var url = BuildUrl(locationFormat);
            _httpClient.SendAsync(method, url, postData, contentType, httpClientCallback);
        }

        public TResponseType PerformRequest<TResponseType>(string method, string locationFormat)
        {
            return PerformRequest<object, TResponseType>(method, locationFormat, null);
        }

        public void PerformRequestAsync<TResponseType>(string method, string locationFormat, Action<TResponseType> apiCallback)
        {
            PerformRequestAsync<object, TResponseType>(method, locationFormat, null, apiCallback);
        }

        public TResponseType PerformRequest<TRequestType, TResponseType>(string method, string locationFormat, TRequestType request) where TRequestType : class
        {
            if(method.NotAccepted())
            {
                throw new ArgumentException("Invalid Http Method - Currently Supported Methods are GET, POST, PUT and HEAD", "method");
            }
             
            var url = BuildUrl(locationFormat);
            var payload = _payloadBuilder.BuildPayload(request);
            var response = _httpClient.Send(method, url, payload);
            return ProcessResponse<TResponseType>(response);
        }

        public void PerformRequestAsync<TRequestType, TResponseType>(string method, string locationFormat, TRequestType request, Action<TResponseType> apiCallback) where TRequestType : class
        {
            if(method.NotAccepted())
            {
                throw new ArgumentException("Invalid Http Method - Currently Supported Methods are GET, POST, PUT and HEAD", "method");
            }

            var url = BuildUrl(locationFormat);
            var payload = _payloadBuilder.BuildPayload(request);
            _httpClient.SendAsync(method, url, payload, responseMessage => apiCallback(ProcessResponse<TResponseType>(responseMessage)));
        }

        private TResponseType ProcessResponse<TResponseType>(HttpResponseMessage response)
        {
            var responseContent = ValidateResponse(response);
            return _payloadBuilder.UnpackResponse<TResponseType>(responseContent);
        }

        private string ValidateResponse(HttpResponseMessage response)
        {
            var responseContent = response.Content;
            ThrowExceptionForExceptionalStatusCodes(response, responseContent);

            if (string.IsNullOrEmpty(responseContent))
            {
                throw new ApiClientException("An attempt was made to deserialize an empty response.", response);
            }

            return responseContent;
        }

        private void ThrowExceptionForExceptionalStatusCodes(HttpResponseMessage response, string content)
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
                        throw ErrorResponseExceptionFactory.CreateException(response, errorsDespiteSuccess);
                    }
                    return;
                case HttpStatusCode.NotFound:
                    throw new ResourceNotFoundException();
                default:
                    var errors = TryExtractErrorsFromResponse(content);
                    throw ErrorResponseExceptionFactory.CreateException(response, errors);
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
                .Replace("{apiKey}", ClientConfiguration.ApiKey)
                .Replace("{apiVersion}", ClientConfiguration.ApiVersion.ToString());

            if (!string.IsNullOrEmpty(ClientConfiguration.WhiteLabelDomain))
            {
                location = AddQueryStringSeperators(location);
                location += "domain=" + ClientConfiguration.WhiteLabelDomain;
            }

			return new Uri(ClientConfiguration.RootDomain + location);
        }

        private static string AddQueryStringSeperators(string location)
        {
            if(location.Contains("?"))
            {
                if (!location.EndsWith("&"))
                {
                    location += "&";
                }
            }
            else
            {
                location += "?";
            }
            return location;
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