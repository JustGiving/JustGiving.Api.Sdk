using System;
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
        
        public DataPackets.HttpResponseMessage Send(DataPackets.HttpRequestMessage httpRequestMessage)
        {
            var request = ToMicrosoftHttpRequest(httpRequestMessage);
            var response = _httpClient.Send(request);
            return ToNativeResponse(response);
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
            return ToNativeResponse(response);
        }

        public DataPackets.HttpResponseMessage Send(string method, Uri uri, DataPackets.HttpContent postData)
        {
            var httpRequestMessage = new HttpRequestMessage(method, uri, HttpContent.Create(postData.Content, postData.ContentType))
                                         {Headers = {ContentType = postData.ContentType}};
            var response = _httpClient.Send(httpRequestMessage);
            return ToNativeResponse(response);
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

        private static DataPackets.HttpResponseMessage ToNativeResponse(HttpResponseMessage response)
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

        public void SendAsync(DataPackets.HttpRequestMessage httpRequestMessage, Action<DataPackets.HttpResponseMessage> httpClientCallback)
        {
            var request = ToMicrosoftHttpRequest(httpRequestMessage);
            var rawRequestData = new AsyncRequest { HttpClientCallback = httpClientCallback };
            _httpClient.BeginSend(request, SendAsyncEnd, rawRequestData);
        }

        public void SendAsync(string method, Uri uri, byte[] postData, string contentType, Action<DataPackets.HttpResponseMessage> httpClientCallback)
        {
            var content = HttpContent.Create(postData, contentType);
            var request = new HttpRequestMessage(method, uri, content);
            var rawRequestData = new AsyncRequest { RawPostData = postData, RawPostDataContentType = contentType, HttpClientCallback = httpClientCallback };
            _httpClient.BeginSend(request, SendAsyncEnd, rawRequestData);
        }

        public void SendAsync(string method, Uri uri, DataPackets.HttpContent postData, Action<DataPackets.HttpResponseMessage> httpClientCallback)
        {
            var httpRequestMessage = new HttpRequestMessage(method, uri, HttpContent.Create(postData.Content, postData.ContentType)) { Headers = { ContentType = postData.ContentType } };
            var rawRequestData = new AsyncRequest { PostData = postData, HttpClientCallback = httpClientCallback };
            _httpClient.BeginSend(httpRequestMessage, SendAsyncEnd, rawRequestData);
        }

        private void SendAsyncEnd(IAsyncResult response)
        {
            var state = (AsyncRequest)response.AsyncState;
            var responseMessage = _httpClient.EndSend(response);
            state.HttpClientCallback(ToNativeResponse(responseMessage));
        }
    }
}
