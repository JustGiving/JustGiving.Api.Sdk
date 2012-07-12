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

            if (response != null && response.Content != null)
            {
                var line = string.Format("{0} {1}: {2}", (int) response.StatusCode, response.StatusCode,
                                            response.Content);

                errorMessageBuilder.AppendLine(line);
            }

            return new ErrorResponseException(response, errors, errorMessageBuilder.ToString());
        }
    }
}