using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Http.Headers;

namespace JustGiving.Api.Sdk.Http.MicrosoftHttp
{
    /// <summary>
    /// The term Zip refers to Compressing, GZipping and Deflating. This class is used in conjunction with Microsoft HTTP to unzip responses
    /// </summary>
    public static class Zip
    {
        public static byte[] GetUnzippedContent(Microsoft.Http.HttpResponseMessage httpResponseMessage)
        {
            var contentCoding = DetectZipFormat(httpResponseMessage.Headers.ContentEncoding);
            return Unzip(contentCoding, httpResponseMessage.Content);
        }

        private static ContentCoding DetectZipFormat(HeaderValues<ContentCoding> headerValues)
        {
            if (headerValues == null || headerValues.Count == 0 || headerValues.Count > 1)
            {
                // AT: To increase our chances of parsing the content, I am assuming more than one content type resolves to identity
                return ContentCoding.Identity;
            }
            var contentCoding = headerValues[0];
            if (contentCoding != ContentCoding.GZip && contentCoding != ContentCoding.Deflate)
            {
                throw new NotSupportedException("the stream compression format is not supported");
            }

            return contentCoding;
        }

        private static byte[] Unzip(ContentCoding contentCoding, Microsoft.Http.HttpContent httpContent)
        {
            if (httpContent == null)
            {
                return null;
            }
            if (contentCoding == ContentCoding.Identity)
            {
                return httpContent.ReadAsByteArray();
            }
            using (var zippedStream = GetUnzipStream(contentCoding, httpContent.ReadAsStream()))
            {
                return ReadAllBytes(zippedStream);
            }
        }

        private static byte[] ReadAllBytes(Stream streamOfUnknownLength)
        {
            // code to read stream of unknown length lifted from http://www.yoda.arachsys.com/csharp/readbinary.html
            // this stuff gives me a headache, sorry

            const int initialLength = 32768;

            var buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = streamOfUnknownLength.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = streamOfUnknownLength.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    var newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }

            // Buffer is now too big. Shrink it.
            var result = new byte[read];
            Array.Copy(buffer, result, read);
            return result;
        }

        private static Stream GetUnzipStream(ContentCoding contentCoding, Stream httpContentStream)
        {
            switch (contentCoding)
            {
                case ContentCoding.GZip:
                    return new GZipStream(httpContentStream, CompressionMode.Decompress);

                case ContentCoding.Deflate:
                    return new DeflateStream(httpContentStream, CompressionMode.Decompress);
                default:
                    throw new NotSupportedException();

            }
        }

    }
}