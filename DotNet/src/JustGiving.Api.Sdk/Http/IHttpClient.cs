using System;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk.Http
{
    public interface IHttpClient : IDisposable
    {
        HttpResponseMessage Send(HttpRequestMessage httpRequestMessage);
        void AddHeader(string key, string value);
        HttpResponseMessage Send(string method, Uri uri, byte[] postData, string contentType);
        HttpResponseMessage Send(string method, Uri uri, Payload postData);
    }
}