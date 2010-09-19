using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class CharityApiTests
    {
        [Test]
        public void RetrieveCharity_IssuedWithKnownId_ReturnsCharity()
        {
            var client = new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1));
            var charityClient = new CharityApi(client);

            var item = charityClient.Retrieve(2050);
        }
    }
}
