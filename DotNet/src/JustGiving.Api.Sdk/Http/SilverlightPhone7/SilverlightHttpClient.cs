using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
using RestSharp;

namespace JustGiving.Api.Sdk.WindowsPhone7.Http.SilverlightPhone7
{
    public class SilverlightHttpClient: IHttpClient
    {
        private readonly Dictionary<string, string> _headers;

        public SilverlightHttpClient()
        {
            _headers = new Dictionary<string, string>();
        }

        public void AddHeader(string key, string value)
        {
            _headers[key] = value;
        }

        public void Dispose()
        {
        }

        public void SendAsync(HttpRequestMessage httpRequestMessage, Action<HttpResponseMessage> httpClientCallback)
        {
            SendAsync(httpRequestMessage.Method, httpRequestMessage.Uri, new HttpContent(httpRequestMessage.Content.Content, httpRequestMessage.Content.ContentType), httpClientCallback);
            throw new NotImplementedException();
        }

        public void SendAsync(string method, Uri uri, byte[] postData, string contentType, Action<HttpResponseMessage> httpClientCallback)
        {
            throw new NotImplementedException();
        }

        public void SendAsync(string method, Uri uri, HttpContent postData, Action<HttpResponseMessage> httpClientCallback)
        {
            var client = new RestClient(uri.Scheme + "://" + uri.Host);
            var request = new RestRequest(uri.AbsolutePath) {RootElement = "Success"};
            foreach(var header in _headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            client.ExecuteAsync(request, callback => SendAsyncEnd(httpClientCallback, callback, method));
        }

        public void SendAsyncEnd(Action<HttpResponseMessage> httpClientCallback, RestResponse response, string method)
        {
            var restResponse = ToNativeResponse(response, method);
            httpClientCallback(restResponse);
        }

        private static HttpResponseMessage ToNativeResponse(RestResponse response, string httpMethod)
        {
            var native = new HttpResponseMessage
                             {
                                 Content =
                                 {
                                     Content = response.Content,
                                     ContentType = response.ContentType
                                 },
                                 Method = httpMethod,
                                 StatusCode = response.StatusCode,
                                 Uri = response.ResponseUri
                             };
            return native;
        }




        public HttpResponseMessage Send(HttpRequestMessage httpRequestMessage)
        {
            throw new NotImplementedException("Not implemented in Silverlight, use Async methods.");
        }

        public HttpResponseMessage Send(string method, Uri uri, byte[] postData, string contentType)
        {
            throw new NotImplementedException("Not implemented in Silverlight, use Async methods.");
        }

        public HttpResponseMessage Send(string method, Uri uri, HttpContent postData)
        {
            throw new NotImplementedException("Not implemented in Silverlight, use Async methods.");
        }
    }
}
