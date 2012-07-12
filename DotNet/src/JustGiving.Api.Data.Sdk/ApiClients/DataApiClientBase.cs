using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public abstract class DataApiClientBase : ApiClientBase
    {
        protected DataApiClientBase(HttpChannel channel)
            : base(channel)
        {
            
        }

        protected static string BaseRoot
        {
            get { return "{apiKey}/v{apiVersion}"; }
        }
    }
}
