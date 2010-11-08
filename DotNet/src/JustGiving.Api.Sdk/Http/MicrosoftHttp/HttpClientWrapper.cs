using System;
using JustGiving.Api.Sdk.Http.DataPackets;
using Microsoft.Http;
using HttpContent = Microsoft.Http.HttpContent;
using HttpRequestMessage = Microsoft.Http.HttpRequestMessage;
using HttpResponseMessage = Microsoft.Http.HttpResponseMessage;

namespace JustGiving.Api.Sdk.Http.MicrosoftHttp
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public DataPackets.HttpResponseMessage Get(string uri)
        {
            var response = _httpClient.Get(uri);
            return ToNativeResponseMessage(response);
        }

        public DataPackets.HttpResponseMessage Get(string uri, string contentType)
        {
            _httpClient.DefaultHeaders.ContentType = contentType;
            var response = _httpClient.Get(uri);
            return ToNativeResponseMessage(response);
        }

        public DataPackets.HttpResponseMessage Post(string url, string contentType, DataPackets.HttpContent body)
        {
            var response = _httpClient.Post(url, contentType, HttpContent.Create(body.Content, body.ContentType));
            return ToNativeResponseMessage(response);
        }

        public DataPackets.HttpResponseMessage Delete(string url)
        {
            var response = _httpClient.Delete(url);
            return ToNativeResponseMessage(response);
        }

        public void SendAsync(DataPackets.HttpRequestMessage httpRequestMessage)
        {
            _httpClient.SendAsync(ToMicrosoftHttpRequest(httpRequestMessage));
        }

        public DataPackets.HttpResponseMessage Send(DataPackets.HttpRequestMessage httpRequestMessage)
        {
            var request = ToMicrosoftHttpRequest(httpRequestMessage);
            var response = _httpClient.Send(request);
            return ToNativeResponseMessage(response);
        }

        public void Put(string url, string contentType, DataPackets.HttpContent body)
        {
            var bodyContent = HttpContent.Create(body.Content, body.ContentType);
            _httpClient.Put(url, contentType, bodyContent);
        }

        public void AddHeader(string key, string value)
        {
            _httpClient.DefaultHeaders.Add(key, value);
        }

        public DataPackets.HttpResponseMessage Send(string method, Uri uri, byte[] postData, string contentType)
        {
            var content = HttpContent.Create(postData, contentType);
            var request = new HttpRequestMessage(method, uri, content);
            var response = _httpClient.Send(request);
            return ToNativeResponseMessage(response);
        }

        public DataPackets.HttpResponseMessage Send(string method, Uri uri, Payload postData)
        {
            var httpRequestMessage = new HttpRequestMessage(method, uri, HttpContent.Create(postData.Content, postData.ContentType))
                                         {Headers = {ContentType = postData.ContentType}};
            var response = _httpClient.Send(httpRequestMessage);
            return ToNativeResponseMessage(response);
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }

        private static HttpRequestMessage ToMicrosoftHttpRequest(DataPackets.HttpRequestMessage httpRequestMessage)
        {
            if (string.IsNullOrEmpty(httpRequestMessage.Content.Content))
            {

                return new HttpRequestMessage(httpRequestMessage.Method, httpRequestMessage.Uri);
            }
            
            return new HttpRequestMessage(httpRequestMessage.Method, httpRequestMessage.Uri,
                                          HttpContent.Create(httpRequestMessage.Content.Content,
                                                             httpRequestMessage.Content.ContentType));
        }

        private static DataPackets.HttpResponseMessage ToNativeResponseMessage(HttpResponseMessage response)
        {
            var responseFormat = new DataPackets.HttpResponseMessage
            {
                Content =
                {
                    Content = response.Content.ReadAsString(),
                    ContentType = response.Content.ContentType
                },
                Method = response.Method,
                Properties = response.Properties,
                StatusCode = response.StatusCode,
                Uri = response.Uri
            };
            return responseFormat;
        }
    }
}
