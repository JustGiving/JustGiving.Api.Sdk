using System;
using System.Text;
using Microsoft.Http;
using Microsoft.Http.Headers;

namespace JustGiving.Api.Sdk.Http
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage Get(string uri)
        {
            return _httpClient.Get(uri);
        }

        public HttpResponseMessage Get(string uri, string contentType)
        {
            _httpClient.DefaultHeaders.ContentType = contentType;
            return _httpClient.Get(uri);
        }

        public HttpResponseMessage Post(string url, string contentType, HttpContent body)
        {
            return _httpClient.Post(url, contentType, body);
        }

        public HttpResponseMessage Delete(string url)
        {
            return _httpClient.Delete(url);
        }

        public void SendAsync(HttpRequestMessage httpRequestMessage)
        {
            _httpClient.SendAsync(httpRequestMessage);
        }

        public HttpResponseMessage Send(HttpRequestMessage httpRequestMessage)
        {
            return _httpClient.Send(httpRequestMessage);
        }

        public void Put(string url, string contentType, HttpContent body)
        {
            _httpClient.Put(url, contentType, body);
        }

        public void AddHeader(string key, string value)
        {
            _httpClient.DefaultHeaders.Add(key, value);
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}
