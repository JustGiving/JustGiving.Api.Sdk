using System;
using System.Net;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Donation;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
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

            var exception = Assert.Throws<ArgumentNullException>(() => api.IsPageShortNameRegistered(pageShortName));

            Assert.That(exception.ParamName, Is.StringContaining("pageShortName"));
            Assert.That(exception.Message, Is.StringContaining("pageShortName cannot be null."));
        }

        [Test]
        public void IsPageShortNameRegistered_WhenProvidedWithValidStringForPageShortName_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<PageRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<PageApi, PageRegistrationConfirmation>(httpClient);
            const string pageShortName = "someName";

            api.IsPageShortNameRegistered(pageShortName);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/fundraising/pages/{3}", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, pageShortName)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("HEAD"));
        }

        [Test]
        public void ListAll_ValidAuthenticationProvided_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummarys>(HttpStatusCode.OK);
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion) { Username = "someUsername", Password = "somePassword" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummarys>(httpClient, config);

            api.ListAll();

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/fundraising/pages", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("GET"));
        }

        [Test]
        public void ListAll_WhenUsernameAuthenticationIsNull_ThrowsException()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummarys>(HttpStatusCode.OK);
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion) { Password = "somePassword" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummarys>(httpClient, config);

            var exception = Assert.Throws<Exception>(() => api.ListAll());

            Assert.That(exception.Message, Is.StringContaining("Authentication required to list pages.  Please set a valid configuration object."));
        }

        [Test]
        public void ListAll_WhenPasswordAuthenticationIsNull_ThrowsException()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummarys>(HttpStatusCode.OK);
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion) { Username = "someUsername" };
            var api = ApiClient.Create<PageApi, FundraisingPageSummarys>(httpClient, config);

            var exception = Assert.Throws<Exception>(() => api.ListAll());

            Assert.That(exception.Message, Is.StringContaining("Authentication required to list pages.  Please set a valid configuration object."));
        }

    }
}
