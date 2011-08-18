using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class SearchApiTestse
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void CharitySearch_KeywordWithKnownResults_SearchResultsPresent(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
            var searchClient = new SearchApi(client);

            var items = searchClient.CharitySearch("cancer");
        }
    }
}
