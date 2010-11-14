using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;

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
        }

        public void SendAsync(string method, Uri uri, byte[] postData, string contentType, Action<HttpResponseMessage> httpClientCallback)
        {
            throw new NotImplementedException();
        }

        private class SlAsync
        {
            public HttpWebRequest WebRequest { get; set; }
            public HttpContent PostData { get; set; }
            public Action<HttpResponseMessage> HttpClientCallback { get; set; }
        }

        public void SendAsync(string method, Uri uri, HttpContent postData, Action<HttpResponseMessage> httpClientCallback)
        {
            var httpRequest = WebRequest.CreateHttp(uri);
            httpRequest.Method = method;

            foreach(var header in _headers)
            {
                httpRequest.Headers[header.Key] = header.Value;
            }

            var rawRequestData = new SlAsync { PostData = postData, HttpClientCallback = httpClientCallback, WebRequest = httpRequest };

            if (method == "PUT" || method == "POST")
            {
                httpRequest.BeginGetRequestStream(WriteStream, rawRequestData);
            }
            else
            {
                httpRequest.BeginGetResponse(ReadCallback, rawRequestData);
            }
        }

        private void WriteStream(IAsyncResult asynchronousResult)
        {
            var slRequest = (SlAsync)asynchronousResult.AsyncState;
            var request = slRequest.WebRequest;
            request.ContentType = slRequest.PostData.ContentType;
            var requestStream = request.EndGetRequestStream(asynchronousResult);
            var writer = new StreamWriter(requestStream);
            writer.Write(slRequest.PostData.Content);
            writer.Close();
            requestStream.Close();
            request.BeginGetResponse(ReadCallback, slRequest);
            
        }

        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            var request = (SlAsync)asynchronousResult.AsyncState;
            var response = (HttpWebResponse)request.WebRequest.EndGetResponse(asynchronousResult);
            SendAsyncEnd(request.HttpClientCallback, response);
        }

        public void SendAsyncEnd(Action<HttpResponseMessage> httpClientCallback, HttpWebResponse response)
        {
            var restResponse = ToNativeResponse(response);
            httpClientCallback(restResponse);
        }

        private static HttpResponseMessage ToNativeResponse(HttpWebResponse response)
        {
            string content;
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                content = streamReader.ReadToEnd();
            }

            var responseFormat = new HttpResponseMessage
            {
                Content =
                {
                    Content = content,
                    ContentType = response.ContentType
                },
                Method = response.Method,
                StatusCode = response.StatusCode,
                Uri = response.ResponseUri
            };
            return responseFormat;
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
