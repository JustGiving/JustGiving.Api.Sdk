using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk
{
    public interface IJustGivingDataClient
    {
        IHttpClient HttpClient { get; }
        HttpChannel HttpChannel { get; }
    }
}