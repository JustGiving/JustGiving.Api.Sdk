using System.Net;
using System.Security.Authentication;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
    public class PageApiTests
    {
        [Test]
        public void ListAllPages_ConfigUserNameEmpty_ThrowsException()
        {
            var config = TestConfiguration(string.Empty, "pass");

            var client = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, FundraisingPageSummaries>(config, client);
            
            var ex = Assert.Throws<AuthenticationException>(() => api.ListAll());
            Assert.That(ex.Message, Is.StringContaining(
                "Authentication required to list pages.  Please set a valid configuration object."));
        }

        [Test]
        public void ListAllPages_ConfigPasswordEmpty_ThrowsException()
        {
            var config = TestConfiguration("test", string.Empty);

            var client = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, FundraisingPageSummaries>(config, client);

            var ex = Assert.Throws<AuthenticationException>(() => api.ListAll());
            Assert.That(ex.Message, Is.StringContaining(
                "Authentication required to list pages.  Please set a valid configuration object."));
        }

        [Test]
        public void ListAllPages_ValidUserNameAndPassword_CallsExpectedUrl()
        {
            var client = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummaries>(config, client);
            api.ListAll();

            var expected = string.Format(
                "{0}{1}/v{2}/fundraising/pages", config.RootDomain, config.ApiKey, config.ApiVersion);
            var url = client.LastRequestedUrl;
            Assert.AreEqual(expected, url);
        }

        [Test]
        public void ListAllPages_ValidUserNameAndPassword_PerformsHttpGet()
        {
            var client = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummaries>(config, client);
            api.ListAll();

            Assert.That(client.LastRequest.Method, Is.StringMatching("GET"));
        }

        private static ClientConfiguration TestConfiguration(string user, string pass)
        {
            return new ClientConfiguration("TEST")
            {
                Username = user,
                Password = pass
            };
        }
    }
}