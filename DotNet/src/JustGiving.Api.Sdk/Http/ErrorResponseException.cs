using System;
using Microsoft.Http;

namespace JustGiving.Api.Sdk.Http
{
    public class ErrorResponseException: Exception
    {
        public Errors Errors { get; set; }
        public HttpResponseMessage Response { get; private set; }

        public ErrorResponseException(HttpResponseMessage response, string message, Errors errors):base(message)
        {
            Errors = errors;
            Response = response;
        }

        public override string ToString()
        {
            return Response.StatusCode + ":" + base.ToString();
        }
    }
}
