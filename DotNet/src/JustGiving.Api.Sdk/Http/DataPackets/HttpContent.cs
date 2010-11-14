namespace JustGiving.Api.Sdk.Http.DataPackets
{
    public class HttpContent
    {
        public string ContentType { get; set; }
        public string Content { get; set; }

        public HttpContent()
        {
        }

        public HttpContent(string content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }
    }
}