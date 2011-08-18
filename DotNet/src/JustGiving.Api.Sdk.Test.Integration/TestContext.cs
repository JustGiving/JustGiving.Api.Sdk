namespace JustGiving.Api.Sdk.Test.Integration
{
    public static class TestContext
    {
        public static string ApiLocation = "https://api.staging.justgiving.com/";
        public static string ApiKey = "8b347861";
        public static string TestUsername = "apiunittests@justgiving.com";
        public static string TestValidPassword = "password";
        public static string TestInvalidPassword = "badPassword";
        public static string CharityTestUserName = "ehaevaj.hbvef@ngazszqoqt.obx.xm";
        public static string CharityTestUserPassword = "zqfed068";
        public static string CharityTestUserPin = "7886";

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
