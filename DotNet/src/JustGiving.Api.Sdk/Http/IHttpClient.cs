using System;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk.Http
{
    public interface IHttpClient : IHttpClientAsync
    {
        HttpResponseMessage Send(HttpRequestMessage httpRequestMessage);
        void AddHeader(string key, string value);
        HttpResponseMessage Send(string method, Uri uri, byte[] postData, string contentType);
        HttpResponseMessage Send(string method, Uri uri, HttpContent postData);
        TimeSpan? ConnectionTimeOut { get; set; }
    }
}