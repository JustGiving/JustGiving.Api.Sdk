using System.Linq;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
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
            
            var items = searchClient.InMemorySearch(null, "James", "Morrison", null);
            Assert.IsTrue(items.Results.Any());
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void InMemorySearch_KeywordWithOnlyFirstName_ReturnsError_NotingLastNameIsMandatory(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);

            var exception = Assert.Throws<ErrorResponseException>(() =>  searchClient.InMemorySearch(null, "James", null, null));

            Assert.That(exception.Errors.Count, Is.EqualTo(1));
            Assert.That(exception.Errors[0].Id, Is.EqualTo("InvalidLastName"));
            Assert.That(exception.Errors[0].Description, Is.EqualTo("The last name of the person you're remembering cannot be empty."));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void InMemorySearch_KeywordWithOnlyLastName_ReturnsError_NotingFirstNameIsMandatory(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);

            var exception = Assert.Throws<ErrorResponseException>(() => searchClient.InMemorySearch(null, null, "Morrison", null));

            Assert.That(exception.Errors.Count, Is.EqualTo(1));
            Assert.That(exception.Errors[0].Id, Is.EqualTo("InvalidFirstName"));
            Assert.That(exception.Errors[0].Description, Is.EqualTo("The first name of the person you're remembering cannot be empty."));
        }
    }
}
