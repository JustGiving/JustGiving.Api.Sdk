using System;

namespace JustGiving.Api.Sdk.Http
{
    public class ApiClientException : Exception
    {
        public Errors Errors { get; set; }
        public DataPackets.HttpResponseMessage Response { get; private set; }
        
        public ApiClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ApiClientException(string message):base(message)
        {
        }

        public ApiClientException(string message, DataPackets.HttpResponseMessage response)
            : this(message, response, null)
        {
        }

        public ApiClientException(string message, DataPackets.HttpResponseMessage response, Errors errors)
            : base(message)
        {
            Errors = errors;
            Response = response;
        }
    }
}