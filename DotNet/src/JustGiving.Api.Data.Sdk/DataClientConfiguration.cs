using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Sdk;

namespace JustGiving.Api.Data.Sdk
{
    public class DataClientConfiguration : ClientConfiguration
    {
        public int CharityId { get; set; }

        //Use this if api configuration in configuration file
        public DataClientConfiguration()
            : base(DataSdkConfigurationManager.GetProperty(x => x.RootDomain), DataSdkConfigurationManager.GetProperty(x => x.ApiKey), DataSdkConfigurationManager.GetProperty(x => x.ApiVersion))
        {
            Username = DataSdkConfigurationManager.GetProperty(x => x.Username);
            Password = DataSdkConfigurationManager.GetProperty(x => x.Password);
            CharityId = DataSdkConfigurationManager.GetProperty(x => x.CharityId);
        }

        public DataClientConfiguration(string rootDomain, string apiKey, int apiVersion) : base(rootDomain, apiKey, apiVersion)
        {
            
        }
    }
}