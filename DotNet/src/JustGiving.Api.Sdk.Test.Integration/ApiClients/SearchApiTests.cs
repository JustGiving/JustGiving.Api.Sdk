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
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);

            //act
            var items = searchClient.CharitySearch("cancer");

            //assert
            Assert.IsTrue(items.Results.Any());
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void EventSearch_KeywordWithKnownResults_SearchResultsPresent(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);

            //act
            var items = searchClient.EventSearch("Test");
            
            //assert
            Assert.IsTrue(items.Results.Any());
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void InMemorySearch_KeywordWithKnownResults_SearchResultsPresent(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client.HttpChannel);
            
            //act
            var items = searchClient.InMemorySearch(null, "test", "test", null);

            //assert
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

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void FundraiserSearch_KeywordWithKnownResult_SearchResultPresent(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var searchResources = new SearchApi(client.HttpChannel);

            //act
            var result = searchResources.FundraiserSearch("pawel");

            //assert
            Assert.IsTrue(result.SearchResults.Any());
        }
    }
}
