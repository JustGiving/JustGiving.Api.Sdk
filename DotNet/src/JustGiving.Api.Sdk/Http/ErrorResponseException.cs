using System;

namespace JustGiving.Api.Sdk.Http
{
    public class ErrorResponseException: Exception
    {
        public Errors Errors { get; set; }
        public DataPackets.HttpResponseMessage Response { get; private set; }

        public ErrorResponseException(DataPackets.HttpResponseMessage response, Errors errors, string message)
            : base(message)
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
