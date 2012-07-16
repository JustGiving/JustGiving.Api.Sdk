using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class InMemoryApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        [Ignore("In-memory route is not yet implemented")]
        public void GetRememberPerson_WhenProvidedWithValidPersonId_ReturnsRememberPersonDetails(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var inMemoryClient = new InMemoryApi(client.HttpChannel);
            int rememberPersonId = 71;

            var response = inMemoryClient.Retrieve(rememberPersonId);

            Assert.AreEqual(response.RememberedPerson.Id, rememberPersonId);
            Assert.That(response.CollectionUri, Is.StringContaining(string.Format("remember/{0}/", response.RememberedPerson.Id)));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        [Ignore("In-memory collection page is not yet implemented")]
        public void GetRememberPersonCollectionData_WhenProvidedWithValidPersonId_ReturnsRememberPersonCollectionData(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
            var inMemoryClient = new InMemoryApi(client.HttpChannel);
            int rememberPersonId = 71;

            var retrieveCollectionData = inMemoryClient.RetrieveCollectionData(rememberPersonId);

            Assert.Greater(retrieveCollectionData.Pages.Length, 0);
        }
    }
}
