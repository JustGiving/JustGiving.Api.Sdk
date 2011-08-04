using System;
using System.Net;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
    [Category("Fast")]
    public class PageApiTests
    {
        [Test]
        public void Create_WhenProvidedWithRequest_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<PageRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, PageRegistrationConfirmation>(httpClient);

            var response = api.Create(new RegisterPageRequest());

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/fundraising/pages" , TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("PUT"));
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
        public void Create_WhenProvidedWithNullRequest_ThrowsArgumentNullException()
        {
            var httpClient = new MockHttpClient<PageRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, PageRegistrationConfirmation>(httpClient);

            var exception = Assert.Throws<ArgumentNullException>(() => api.Create(null));

            Assert.That(exception.ParamName, Is.StringContaining("request"));
            Assert.That(exception.Message, Is.StringContaining("Request cannot be null."));
        }

        [TestCase("")]
        [TestCase(null)]
        public void IsPageShortNameRegistered_WhenProvidedWithNullOrEmptyPageShortName_ThrowsArgumentNullException(string pageShortName)
        {
            var httpClient = new MockHttpClient<PageRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, PageRegistrationConfirmation>(httpClient);

            var exception = Assert.Throws<ArgumentNullException>(() => api.IsPageShortNameRegistered(pageShortName, null));

            Assert.That(exception.ParamName, Is.StringContaining("pageShortName"));
            Assert.That(exception.Message, Is.StringContaining("pageShortName cannot be null."));
        }

        [TestCase("")]
        [TestCase(null)]
        public void IsPageShortNameRegistered_WhenProvidedWithValidStringForPageShortName_CallsExpectedUrl(string domain)
        {
            var httpClient = new MockHttpClient<PageRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, PageRegistrationConfirmation>(httpClient);
            const string pageShortName = "someName";

            api.IsPageShortNameRegistered(pageShortName, domain);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/fundraising/pages/{3}", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, pageShortName)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("HEAD"));
        }

        [Test]
        public void IsPageShortNameRegistered_WhenProvidedWithValidStringForPageShortNameAndDomain_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<PageRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, PageRegistrationConfirmation>(httpClient);
            const string pageShortName = "someName";
            const string domain = "somedomain.com";

            api.IsPageShortNameRegistered(pageShortName, domain);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/fundraising/pages/{3}?domain={4}", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, pageShortName, domain)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("HEAD"));
        }


        [Test]
        public void ListAll_WhenUsernameAuthenticationIsNull_ThrowsException()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion) { Password = "somePassword" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummaries>(config, httpClient);

            var exception = Assert.Throws<Exception>(() => api.ListAll());

            Assert.That(exception.Message, Is.StringContaining("Authentication required to list pages.  Please set a valid configuration object."));
        }

        [Test]
        public void ListAll_WhenPasswordAuthenticationIsNull_ThrowsException()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion) { Username = "someUsername" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummaries>(config, httpClient);

            var exception = Assert.Throws<Exception>(() => api.ListAll());

            Assert.That(exception.Message, Is.StringContaining("Authentication required to list pages.  Please set a valid configuration object."));
        }

    }
}
