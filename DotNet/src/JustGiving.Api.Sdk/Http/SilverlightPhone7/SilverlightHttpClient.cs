using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Get(string uri, string contentType)
        {
            throw new NotImplementedException();

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Accept = contentType;
            request.ContentType = contentType;
            IAsyncResult asyncResult;
            asyncResult = request.BeginGetResponse(result => { asyncResult = result; }, request);

            asyncResult.AsyncWaitHandle.WaitOne();

            var stateRequest = (HttpWebRequest)asyncResult.AsyncState;
            var response = stateRequest.EndGetResponse(asyncResult);

            return ToNativeResponse(response, "GET");

        }

        public HttpResponseMessage Send(HttpRequestMessage httpRequestMessage)
        {
            throw new NotImplementedException();
        }

        public void AddHeader(string key, string value)
        {
            _headers[key] = value;
        }

        public HttpResponseMessage Send(string method, Uri uri, byte[] postData, string contentType)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage Send(string method, Uri uri, Payload postData)
        {
            throw new NotImplementedException();

            var client = new RestClient(uri.Scheme + "://" + uri.Host);
            var request = new RestRequest(uri.AbsolutePath) {RootElement = "Success"};

            var finished = false;
            var response = new HttpResponseMessage();
            client.ExecuteAsync(request, callback => { finished = true; response = ToNativeResponse(callback.Content, method); });
            
            while(!finished)
            {
                Thread.Sleep(50);
            }
            
            return response;
            
        }

        private HttpResponseMessage ToNativeResponse(string response, string method)
        {
            return new HttpResponseMessage();
        }

        private static HttpResponseMessage ToNativeResponse(WebResponse response, string httpMethod)
        {
            var native = new HttpResponseMessage
                             {
                                 Content =
                                     {
                                         ContentType = response.ContentType,
                                         Content = new StreamReader(response.GetResponseStream()).ReadToEnd()
                                     },
                                 Method = httpMethod
                             };
            return native;
        }
    }
}
