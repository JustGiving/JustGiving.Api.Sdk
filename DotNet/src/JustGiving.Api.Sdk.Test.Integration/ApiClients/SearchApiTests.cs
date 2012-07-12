using System.Linq;
using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class SearchApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void CharitySearch_KeywordWithKnownResults_SearchResultsPresent(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);

            var items = searchClient.CharitySearch("cancer");

            Assert.IsTrue(items.Results.Any());
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void EventSearch_KeywordWithKnownResults_SearchResultsPresent(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);

            var items = searchClient.EventSearch("running");
            Assert.IsTrue(items.Results.Any());
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void InMemorySearch_KeywordWithKnownResults_SearchResultsPresent(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);
            
            var items = searchClient.InMemorySearch(null, "James", null, null);
            Assert.IsTrue(items.Results.Any());
        }
    }
}
