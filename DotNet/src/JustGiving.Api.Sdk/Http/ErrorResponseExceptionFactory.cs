using System.Text;
using Microsoft.Http;

namespace JustGiving.Api.Sdk.Http
{
    public static class ErrorResponseExceptionFactory
    {
        public static ErrorResponseException CreateException(HttpResponseMessage response, string responseContent, Errors errors)
        {
            var errorMessageBuilder = new StringBuilder();

            if (response != null)
            {
                errorMessageBuilder.AppendLine(response.ToString());
            }

            if (errors != null)
            {
                foreach (var error in errors)
                {
                    errorMessageBuilder.AppendLine(error.Id + ": " + error.Description);
                }
            }

            if (!string.IsNullOrEmpty(responseContent))
            {
                errorMessageBuilder.AppendLine(responseContent);
            }

            return new ErrorResponseException(response, errors, errorMessageBuilder.ToString());
        }
    }
}