using System;
using System.Net;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk
{
    public class AsyncRequest
    {
        public byte[] RawPostData { get; set; }
        public string RawPostDataContentType { get; set; }
        public Action<HttpResponseMessage> HttpClientCallback { get; set; }
        public HttpWebRequest WebRequest { get; set; }
        public HttpContent PostData { get; set; }
    }
}