using System.Configuration;
using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public abstract class DataApiClientBase : ApiClientBase
    {
        private static JustGivingDataSdkConfiguration _configuration;
        private static DataClientConfiguration DataConfiguration { get; set; }
        protected DataApiClientBase(HttpChannel channel)
            : base(channel)
        {
            _configuration = ConfigurationManager.GetSection("justGivingDataSdk") as JustGivingDataSdkConfiguration;
        }

        protected DataApiClientBase(HttpChannel channel, DataClientConfiguration dataConfiguration)
            : base(channel)
        {
            DataConfiguration = dataConfiguration;
        }

        protected static string BaseRoot
        {
            get
            {
                var baseRoot = "{apiKey}/v{apiVersion}";
                
                InitialiseConfiguration();

                if (_configuration != null && _configuration.CharityId != 0)
                {
                    baseRoot = baseRoot + "/charity/" + _configuration.CharityId;
                }
                if (DataConfiguration != null && DataConfiguration.CharityId != 0)
                {
                    baseRoot = baseRoot + "/charity/" + DataConfiguration.CharityId;
                }
                return baseRoot;
            }
        }

        private static void InitialiseConfiguration()
        {
            if(_configuration == null)
            {
                _configuration = ConfigurationManager.GetSection("justGivingDataSdk") as JustGivingDataSdkConfiguration;
            }
        }

        protected static void ReloadDataConfiguration(DataClientConfiguration dataConfiguration)
        {
            DataConfiguration = dataConfiguration;
        }
    }
}
