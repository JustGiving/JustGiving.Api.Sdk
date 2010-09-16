using System;
using Microsoft.Http;

namespace JustGiving.Api.Sdk.Http
{
    public class ApiClientException : Exception
    {
        public Errors Errors { get; set; }
        public HttpResponseMessage Response { get; private set; }
        
        public ApiClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ApiClientException(string message):base(message)
        {
        }

        public ApiClientException(string message, HttpResponseMessage response)
            : this(message, response, null)
        {
        }

        public ApiClientException(string message, HttpResponseMessage response, Errors errors)
            : base(message)
        {
            Errors = errors;
            Response = response;
        }
    }
}