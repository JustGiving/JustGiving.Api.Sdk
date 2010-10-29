using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class CharityApiTests : ApiClientTestsBase
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveCharity_IssuedWithKnownId_ReturnsCharity(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var charityClient = new CharityApi(client);

            var item = charityClient.Retrieve(2050);

            Assert.IsNotNull(item);
            Assert.That(item.Name, Is.StringContaining("The Demo Charity"));
        }
    }
}
