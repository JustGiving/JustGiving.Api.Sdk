using System.Net;
using System.Web;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Search;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
    public class SearchApiTests
    {
        [Test]
        public void CharitySearch_EmptySearchTerms_DoesNotCallApi()
        {
            var httpClient = new MockHttpClient<CharitySearchResults>(HttpStatusCode.OK);
            var api = ApiClient.Create<SearchApi, CharitySearchResults>(httpClient);

            api.CharitySearch(string.Empty);

            Assert.Null(httpClient.LastRequest);
        }

        [Test]
        public void CharitySearch_SearchTermsSpecified_UrlEncodesSearchTerm()
        {
            var httpClient = new MockHttpClient<CharitySearchResults>(HttpStatusCode.OK);
            var api = ApiClient.Create<SearchApi, CharitySearchResults>(httpClient);

            const string SEARCH = "test/value and &something";
            var expected = HttpUtility.UrlEncode(SEARCH);
            api.CharitySearch(SEARCH);
            var actual = httpClient.LastRequestedUrl;
            Assert.That(actual, Is.StringContaining(expected));
        }
    }
}