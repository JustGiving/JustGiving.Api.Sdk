using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk.Http
{
    public class MultiformatPayloadBuilder
    {
        private readonly ClientConfiguration _clientConfiguration;
        public string XmlContentType { get; private set; }
        public string JsonContentType { get; private set; }

        public MultiformatPayloadBuilder(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            XmlContentType = "application/xml";
            JsonContentType = "application/json";
        }

        public HttpContent BuildPayload<TPayloadType>(TPayloadType objectToSerialise) where TPayloadType : class
        {
            string contentType;
            string payload;

            if (_clientConfiguration.WireDataFormat == WireDataFormat.Xml)
            {
                if(objectToSerialise == null)
                {
                    return new HttpContent { Content = string.Empty, ContentType = XmlContentType };
                }

                payload = SerializeContentToXml(objectToSerialise);
                contentType = XmlContentType;
            }
            else
            {
                if (objectToSerialise == null)
                {
                    return new HttpContent { Content = string.Empty, ContentType = JsonContentType };
                }

                payload = SerializeContentToJson(objectToSerialise);
                contentType = JsonContentType;
            }

            return new HttpContent { Content = payload, ContentType = contentType };
        }

        public T UnpackResponse<T>(string responseContent)
        {
            if (_clientConfiguration.WireDataFormat == WireDataFormat.Xml)
            {
                return DeserializeContentFromXml<T>(responseContent);
            }

            if (_clientConfiguration.WireDataFormat == WireDataFormat.Json)
            {
                return DeserializeContentFromJson<T>(responseContent);
            }

            throw new NotImplementedException("Unsupported response format.");
        }

        private static string SerializeContentToXml<TPayloadType>(TPayloadType objectToSerialise)
        {
            using (var memoryStream = new MemoryStream())
            {
                var dataContractSerializer = new DataContractSerializer(objectToSerialise.GetType());
                dataContractSerializer.WriteObject(memoryStream, objectToSerialise);
                memoryStream.Flush();
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string SerializeContentToJson<TPayloadType>(TPayloadType objectToSerialise)
        {
            using (var memoryStream = new MemoryStream())
            {
                var dataContractSerializer = new DataContractJsonSerializer(objectToSerialise.GetType());
                dataContractSerializer.WriteObject(memoryStream, objectToSerialise);
                memoryStream.Flush();
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static TResponseType DeserializeContentFromXml<TResponseType>(string content)
        {
            try
            {
                var reader = new DataContractSerializer(typeof(TResponseType));
                var byteArray = Encoding.UTF8.GetBytes(content);
                var stream = new MemoryStream(byteArray);
                return (TResponseType)reader.ReadObject(stream);
            }
            catch (Exception ex)
            {
                var exception = new ApiClientException("An error occured while deserializing the incoming response", ex);
                ex.Data.Add("RawContent", content);
                throw exception;
            }
        }

        private static TResponseType DeserializeContentFromJson<TResponseType>(string content)
        {
            try
            {
                var reader = new DataContractJsonSerializer(typeof(TResponseType));
                var byteArray = Encoding.UTF8.GetBytes(content);
                var stream = new MemoryStream(byteArray);
                return (TResponseType)reader.ReadObject(stream);
            }
            catch (Exception ex)
            {
                var exception = new ApiClientException("An error occured while deserializing the incoming response", ex);
                ex.Data.Add("RawContent", content);
                throw exception;
            }
        }
    }
}
