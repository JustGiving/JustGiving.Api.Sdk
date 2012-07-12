using System;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    internal static class ContentTypes
    {
        public static string GetAcceptContentType(DataFileFormat format)
        {
            switch (format)
            {
                case DataFileFormat.excel:
                    return "application/vnd.ms-excel";
                case DataFileFormat.csv:
                    return "text/csv";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
