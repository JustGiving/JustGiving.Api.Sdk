using System.Text;

namespace JustGiving.Api.Sdk.Http
{
    public static class ErrorResponseExceptionFactory
    {
        public static ErrorResponseException CreateException(DataPackets.HttpResponseMessage response, Errors errors)
        {
            var errorMessageBuilder = new StringBuilder();

            if (errors != null)
            {
                foreach (var error in errors)
                {
                    errorMessageBuilder.AppendLine(error.Id + ": " + error.Description);
                }
            }

            if (response != null && !string.IsNullOrEmpty(response.Content.Content))
            {
                errorMessageBuilder.AppendLine(response.Content.Content);
            }

            return new ErrorResponseException(response, errors, errorMessageBuilder.ToString());
        }
    }
}