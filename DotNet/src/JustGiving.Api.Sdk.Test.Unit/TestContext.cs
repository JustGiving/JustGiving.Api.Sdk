namespace JustGiving.Api.Sdk.Test.Unit
{
    public static class TestContext
    {
        public static string ApiLocation = "https://api-staging.justgiving.com/";
        public static string ApiKey = "NOKEY";
        public static string TestUsername = "apitests@justgiving.com";
        public static string TestValidPassword = "password";
        public static string TestInvalidPassword = "badPassword";
        public static int ApiVersion = 1;

        public static ClientConfiguration Configuration
        {
            get
            {
                var configuration = new ClientConfiguration(ApiLocation, ApiKey, ApiVersion)
                                        {
                                            Username = TestUsername,
                                            Password = TestValidPassword
                                        };
                return configuration;
            }
        }
    }
}
