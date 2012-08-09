using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk.WindowsPhone7.Http.SilverlightPhone7
{
    public class SilverlightHttpClient: IHttpClient
    {
        private readonly Dictionary<string, string> _headers;

        public TimeSpan? ConnectionTimeOut { get; set; }

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
            var httpRequest = ConfigureWebRequest(uri, method);
            var rawRequestData = new AsyncRequest { RawPostData = postData, RawPostDataContentType = contentType ,HttpClientCallback = httpClientCallback, WebRequest = httpRequest };
            BeginRequest(method, httpRequest, rawRequestData);
        }

        public void SendAsync(string method, Uri uri, HttpContent postData, Action<HttpResponseMessage> httpClientCallback)
        {
            var httpRequest = ConfigureWebRequest(uri, method);
            var rawRequestData = new AsyncRequest { PostData = postData, HttpClientCallback = httpClientCallback, WebRequest = httpRequest };
            BeginRequest(method, httpRequest, rawRequestData);
        }

        private HttpWebRequest ConfigureWebRequest(Uri uri, string method)
        {
            var httpRequest = WebRequest.CreateHttp(uri);
            httpRequest.Method = method;
            AddHeaders(httpRequest);
            return httpRequest;
        }

        private void AddHeaders(WebRequest httpRequest)
        {
            foreach(var header in _headers)
            {
                httpRequest.Headers[header.Key] = header.Value;
            }
        }

        private void BeginRequest(string method, HttpWebRequest httpRequest, AsyncRequest rawRequestData)
        {
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
            var slRequest = (AsyncRequest)asynchronousResult.AsyncState;
            var request = slRequest.WebRequest;
            
            var requestStream = request.EndGetRequestStream(asynchronousResult);
            var writer = new StreamWriter(requestStream);

            if (slRequest.PostData != null)
            {
                request.ContentType = slRequest.PostData.ContentType;
                writer.Write(slRequest.PostData.Content);
            }
            else if(slRequest.RawPostData.Length > 0)
            {
                request.ContentType = slRequest.RawPostDataContentType;
                writer.Write(slRequest.RawPostData);
            }

            writer.Close();
            requestStream.Close();
            request.BeginGetResponse(ReadCallback, slRequest);
            
        }

        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            var request = (AsyncRequest)asynchronousResult.AsyncState;

            try
            {
                var response = (HttpWebResponse) request.WebRequest.EndGetResponse(asynchronousResult);
                SendAsyncEnd(request.HttpClientCallback, response);
            }
            catch(WebException ex)
            {
                SendAsyncEnd(request.HttpClientCallback, ex.Response);
            }
            
        }

        private void SendAsyncEnd(Action<HttpResponseMessage> httpClientCallback, WebResponse response)
        {
            var restResponse = ToNativeResponse(response);
            Deployment.Current.Dispatcher.BeginInvoke(() => httpClientCallback(restResponse));
        }

        public void SendAsyncEnd(Action<HttpResponseMessage> httpClientCallback, HttpWebResponse response)
        {
            var restResponse = ToNativeResponse(response);
            Deployment.Current.Dispatcher.BeginInvoke(() => httpClientCallback(restResponse));
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
                    Content = Encoding.UTF8.GetBytes(content),
                    ContentType = response.ContentType
                },
                Method = response.Method,
                StatusCode = response.StatusCode,
                Uri = response.ResponseUri
            };
            return responseFormat;
        }

        private static HttpResponseMessage ToNativeResponse(WebResponse response)
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
                    Content = Encoding.UTF8.GetBytes(content),
                    ContentType = response.ContentType
                },
                StatusCode = HttpStatusCode.InternalServerError,
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
