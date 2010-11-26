namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    public abstract class ApiClientTestsBase
    {
        public JustGivingClient CreateClientNoCredentials(WireDataFormat wireDataFormat)
        {
            return new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1) { WireDataFormat = wireDataFormat });
        }

        public JustGivingClient CreateClientValidCredentials(WireDataFormat wireDataFormat)
        {
            return new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                                            {
                                                Username = TestContext.TestUsername, 
                                                Password = TestContext.TestValidPassword,
                                                WireDataFormat = wireDataFormat
                                            });

        }

        public JustGivingClient CreateClientInvalidCredentials(WireDataFormat wireDataFormat)
        {
            return new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                                            {
                                                Username = TestContext.TestUsername,
                                                Password = TestContext.TestValidPassword,
                                                WireDataFormat = wireDataFormat
                                            });
        }

    }
}
