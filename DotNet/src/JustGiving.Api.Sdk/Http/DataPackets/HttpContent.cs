using System.Text;

namespace JustGiving.Api.Sdk.Http.DataPackets
{
    public class HttpContent
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public Encoding Encoding { get; set; }

        public HttpContent()
        {
            Content = new byte[0];
        }

        public HttpContent(string contentType)
        {
            Content = new byte[0];
            ContentType = contentType;
        }

        public HttpContent(byte[] content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }

        public override string ToString()
        {
            if (Encoding != null)
            {
                return Encoding.GetString(Content);
            }

            return TextEncoding.Default.GetString(Content);
        }

        public static implicit operator string(HttpContent content)
        {
            return content.ToString();
        }
    }
}