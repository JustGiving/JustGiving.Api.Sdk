using System.Configuration;
using JustGiving.Api.Sdk.Test.Integration.Configuration;

namespace JustGiving.Api.Sdk.Test.Integration
{
    public static class TestContext
    {
        public static string ApiLocation = "http://api.local.justgiving.com/";
        //public static string ApiLocation = "https://api-staging.justgiving.com/";
        public static string ApiKey = "8b347861";
        public static string TestUsername = "apiunittests@justgiving.com";
        public static string TestValidPassword = "password";
        public static string TestInvalidPassword = "badPassword";

        public static string CharityTestUserName
        {
            get
            {
                var configuration = (ITestConfigurations) ConfigurationManager.GetSection("testConfirguations");
                return (configuration != null ? configuration.CharityUserUserName : "hsusa.vowoar@gqnuxwuwgc.chg");
            }
        }

        public static string CharityTestUserPassword = "zcnfh377";
        public static string CharityTestUserPin = "2050";

        //hsusa.vowoar@gqnuxwuwgc.chg	zcnfh377

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
    }
}
