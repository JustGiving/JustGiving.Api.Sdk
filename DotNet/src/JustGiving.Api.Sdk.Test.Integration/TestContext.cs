using System.Configuration;
using JustGiving.Api.Sdk.Test.Integration.Configuration;

namespace JustGiving.Api.Sdk.Test.Integration
{
    public static class TestContext
    {
        public static string TestUsername = "apisdktester@justgiving.com";
        public static string TestValidPassword = "api5dkt3st3r";
        public static string TestInvalidPassword = "badPassword";
        public static string CharityTestUserPassword = "zcnfh377";
        public static string CharityTestUserPin = "2050";
        public static string ApiKey = "c064cbf2";
   
        public static string ApiLocation
        {
            get 
            { 
                return TestConfigurationsHelper.GetProperty(x => x.ApiLocation); 
            }
        }

        public static string CharityTestUserName
        {
            get
            {
                var configuration = (ITestConfigurations) ConfigurationManager.GetSection("testConfirguations");
                return (configuration != null ? configuration.CharityUserUserName : "hsusa.vowoar@gqnuxwuwgc.chg");
            }
        }

        public static string RflUserName { get { return TestConfigurationsHelper.GetProperty(x => x.RflUsernName) ?? "rfltester@justgiving.com"; } }



        public static JustGivingClient CreateClientNoCredentials(WireDataFormat wireDataFormat)
        {
            var cfg = new ClientConfiguration(ApiLocation, ApiKey, 1) { WireDataFormat = wireDataFormat };
            return new JustGivingClient(cfg);
        }

        public static JustGivingClient CreateClientValidCredentials(WireDataFormat wireDataFormat)
        {
            var cfg = new ClientConfiguration(ApiLocation, ApiKey, 1)
            {
                Username = TestUsername,
                Password = TestValidPassword,
                WireDataFormat = wireDataFormat
            };

            return new JustGivingClient(cfg);
        }

        public static JustGivingClient CreateClientInvalidCredentials(WireDataFormat wireDataFormat)
        {

            var cfg = new ClientConfiguration(ApiLocation, ApiKey, 1)
            {
                Username = TestUsername,
                Password = TestValidPassword,
                WireDataFormat = wireDataFormat
            };
            return new JustGivingClient(cfg);
        }

        public static JustGivingClient CreateClientValidRflCredentials(WireDataFormat wireDataFormat)
        {
            var cfg = new ClientConfiguration(ApiLocation, ApiKey, 1)
            {
                Username = RflUserName,
                Password = TestValidPassword,
                WireDataFormat = wireDataFormat
            };
            return new JustGivingClient(cfg);
        }
    }
}
