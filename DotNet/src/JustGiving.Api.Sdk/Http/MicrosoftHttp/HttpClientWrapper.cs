using System;
using System.Net.Mime;
using System.Text;
using Microsoft.Http;
using Microsoft.Http.Headers;
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
        
        public TimeSpan? ConnectionTimeOut
        {
            get
            {
                return _httpClient.TransportSettings.ConnectionTimeout;
            }
            set
            {
                _httpClient.TransportSettings.ConnectionTimeout = value;
            }
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
            if (httpRequestMessage.Content == null || httpRequestMessage.Content.Content.Length == 0)
            {
                return new HttpRequestMessage(httpRequestMessage.Method, httpRequestMessage.Uri)
                            .WithAcceptHeader(httpRequestMessage);
            }

            return new HttpRequestMessage(httpRequestMessage.Method, httpRequestMessage.Uri, HttpContent.Create(httpRequestMessage.Content.Content, httpRequestMessage.Content.ContentType))
                                           .WithAcceptHeader(httpRequestMessage);
        }

        
        private static DataPackets.HttpResponseMessage ToNativeResponse(HttpResponseMessage response)
        {
            var responseFormat = new DataPackets.HttpResponseMessage
            {
                Content =
                {
                    Content = Zip.GetUnzippedContent(response),
                    ContentType = response.Content.ContentType,
                    Encoding = GetEncoding(response.Content.ContentType)
                },
                Method = response.Method,
                Properties = response.Properties,
                StatusCode = response.StatusCode,
                Uri = response.Uri
            };
            return responseFormat;
        }

        private static Encoding GetEncoding(string contentTypeString)
        {
            if (string.IsNullOrEmpty(contentTypeString))
            {
                return TextEncoding.Default;
            }

            var contentType = new ContentType(contentTypeString);
            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                return TextEncoding.Default;
            }
            switch (contentType.CharSet.ToLower())
            {
                case "utf-16":
                    return Encoding.Unicode;
                case "utf-8":
                    return Encoding.UTF8;
                default:
                    throw new NotSupportedException(string.Format("this charset {0} is not supported", contentType.CharSet));
            }
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

    public static class HttpRequestExtensions
    {
        public static HttpRequestMessage WithAcceptHeader(this HttpRequestMessage request, DataPackets.HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage != null && !string.IsNullOrEmpty(httpRequestMessage.AcceptContentType))
                request.Headers.Accept = new HeaderValues<StringWithOptionalQuality> { httpRequestMessage.AcceptContentType };

            return request;
        }

    }
}
