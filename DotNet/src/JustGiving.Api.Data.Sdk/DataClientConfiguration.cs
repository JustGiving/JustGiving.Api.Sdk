using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Sdk;

namespace JustGiving.Api.Data.Sdk
{
    public class DataClientConfiguration : ClientConfiguration
    {
        //Use this if api configuration in configuration file
        public DataClientConfiguration()
            : base(DataSdkConfigurationManager.GetProperty(x => x.RootDomain), DataSdkConfigurationManager.GetProperty(x => x.ApiKey), DataSdkConfigurationManager.GetProperty(x => x.ApiVersion))
        {
        }

        public DataClientConfiguration(string rootDomain, string apiKey, int apiVersion) : base(rootDomain, apiKey, apiVersion)
        {
            
        }
    }
}