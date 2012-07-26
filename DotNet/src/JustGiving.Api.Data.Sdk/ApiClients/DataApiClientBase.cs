using System.Configuration;
using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public abstract class DataApiClientBase : ApiClientBase
    {
        private static JustGivingDataSdkConfiguration _configuration;

        protected DataApiClientBase(HttpChannel channel)
            : base(channel)
        {
            _configuration = ConfigurationManager.GetSection("justGivingDataSdk") as JustGivingDataSdkConfiguration;
        }

        protected static string BaseRoot
        {
            get
            {
                var baseRoot = "{apiKey}/v{apiVersion}";
                
                InitialiseConfiguration();

                if (_configuration != null && _configuration.CharityId != 0)
                    baseRoot = baseRoot + "/charity/" + _configuration.CharityId;

                return baseRoot;
            }
        }

        private static void InitialiseConfiguration()
        {
            if(_configuration == null)
                _configuration = ConfigurationManager.GetSection("justGivingDataSdk") as JustGivingDataSdkConfiguration;
        }
    }
}
