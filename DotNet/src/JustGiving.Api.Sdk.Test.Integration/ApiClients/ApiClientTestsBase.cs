namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    public abstract class ApiClientTestsBase
    {
        public JustGivingClient CreateClientNoCredentials(WireDataFormat wireDataFormat)
        {
            return CreateClientNoCredentials(wireDataFormat, null);
        }

        public JustGivingClient CreateClientNoCredentials(WireDataFormat wireDataFormat, string premiumDomain)
        {
            var cfg = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1) { WireDataFormat = wireDataFormat };
            
            if (premiumDomain != null)
            {
                cfg.PremiumDomain = premiumDomain;
            }

            return new JustGivingClient(cfg);
        }

        public JustGivingClient CreateClientValidCredentials(WireDataFormat wireDataFormat)
        {
            return CreateClientValidCredentials(wireDataFormat, null);
        }

        public JustGivingClient CreateClientValidCredentials(WireDataFormat wireDataFormat, string premiumDomain)
        {
            var cfg = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                          {
                              Username = TestContext.TestUsername,
                              Password = TestContext.TestValidPassword,
                              WireDataFormat = wireDataFormat
                          };

            if (premiumDomain != null)
            {
                cfg.PremiumDomain = premiumDomain;
            }

            return new JustGivingClient(cfg);

        }

        public JustGivingClient CreateClientInvalidCredentials(WireDataFormat wireDataFormat)
        {
            return CreateClientInvalidCredentials(wireDataFormat, null);
        }

        public JustGivingClient CreateClientInvalidCredentials(WireDataFormat wireDataFormat, string premiumDomain)
        {
            var cfg = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                {
                    Username = TestContext.TestUsername,
                    Password = TestContext.TestValidPassword,
                    WireDataFormat = wireDataFormat
                };

            if(premiumDomain != null)
            {
                cfg.PremiumDomain = premiumDomain;
            }

            return new JustGivingClient(cfg);
        }

    }
}
