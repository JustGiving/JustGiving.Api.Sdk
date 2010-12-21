namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    public abstract class ApiClientTestsBase
    {
        public JustGivingClient CreateClientNoCredentials(WireDataFormat wireDataFormat)
        {
            var cfg = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1) { WireDataFormat = wireDataFormat };
            return new JustGivingClient(cfg);
        }

        public JustGivingClient CreateClientValidCredentials(WireDataFormat wireDataFormat)
        {
            var cfg = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                          {
                              Username = TestContext.TestUsername,
                              Password = TestContext.TestValidPassword,
                              WireDataFormat = wireDataFormat
                          };

            return new JustGivingClient(cfg);
        }

        public JustGivingClient CreateClientInvalidCredentials(WireDataFormat wireDataFormat)
        {
            var cfg = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                {
                    Username = TestContext.TestUsername,
                    Password = TestContext.TestValidPassword,
                    WireDataFormat = wireDataFormat
                };
            return new JustGivingClient(cfg);
        }

    }
}
