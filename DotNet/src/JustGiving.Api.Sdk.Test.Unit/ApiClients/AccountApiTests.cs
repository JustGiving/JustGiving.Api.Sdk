using System;
using System.Net;
using GG.Api.Sdk.Test.Unit;
using GG.Api.Sdk.Test.Unit.ApiClients;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.MicrosoftHttp;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
    [Category("Fast")]
    public class AccountApiTests
    {
    	private ClientConfiguration _config;
    	private HttpChannel _httpChannel;
    	private AccountApi _api;

    	[SetUp]
		public void SetUp()
		{
			_config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion);
			_httpChannel = new HttpChannel(_config, new HttpClientWrapper());
			_api = new AccountApi(_httpChannel);
		}

        [Test]
        public void Create_WhenProvidedWithNullRequest_ThrowsArgumentNullException()
        {
			var exception = Assert.Throws<ArgumentNullException>(() => _api.Create(null));

            Assert.That(exception.ParamName, Is.StringContaining("request"));
            Assert.That(exception.Message, Is.StringContaining("Request cannot be null"));
        }

        [Test]
        public void Create_WhenProvidedWithRequest_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<AccountRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, AccountRegistrationConfirmation>(httpClient);
            var request = new CreateAccountRequest();

            api.Create(request);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("PUT"));
        }

        [Test]
        public void CreateAsync_WhenProvidedWithRequest_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<AccountRegistrationConfirmation>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, AccountRegistrationConfirmation>(httpClient);
            var request = new CreateAccountRequest();

            api.CreateAsync(request, response => {});

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("PUT"));
        }


        [TestCase("")]
        [TestCase(null)]
        public void ListAllPages_WhenProvidedWithNullOrEmptyEmail_ThrowsArgumentNullException(string email)
        {
			var exception = Assert.Throws<ArgumentNullException>(() => _api.ListAllPages(email));

            Assert.That(exception.ParamName, Is.StringContaining("email"));
            Assert.That(exception.Message, Is.StringContaining("Email cannot be null or empty."));
        }

        [Test]
        public void ListAllPages_WhenProvidedWithEmail_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, FundraisingPageSummaries>(httpClient);
            const string email = "some@email.com";

            api.ListAllPages(email);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account/{3}/pages", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, email)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("GET"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void IsEmailRegistered_WhenProvidedWithNullOrEmptyEmail_ThrowsArgumentNullException(string email)
        {
			var exception = Assert.Throws<ArgumentNullException>(() => _api.IsEmailRegistered(email));

            Assert.That(exception.ParamName, Is.StringContaining("email"));
            Assert.That(exception.Message, Is.StringContaining("Email cannot be null or empty."));
        }

        [Test]
        public void IsEmailRegistered_WhenProvidedWithEmail_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummaries>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, FundraisingPageSummaries>(httpClient);
            const string email = "some@email.com";

            api.IsEmailRegistered(email);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account/{3}", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, email)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("HEAD"));
        }


        [TestCase("")]
        [TestCase(null)]
        public void RequestPasswordReminder_WhenProvidedWithNullOrEmptyEmail_ThrowsArgumentNullException(string email)
        {
			var exception = Assert.Throws<ArgumentNullException>(() => _api.RequestPasswordReminder(email));

            Assert.That(exception.ParamName, Is.StringContaining("email"));
            Assert.That(exception.Message, Is.StringContaining("Email cannot be null or empty."));
        }

        [Test]
        public void RequestPasswordReminder_WhenProvidedWithEmail_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<object>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, object>(httpClient);
            const string email = "some@email.com";

            api.RequestPasswordReminder(email);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account/{3}/requestpasswordreminder", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, email)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("GET"));
        }

        [Test]
        public void RequestPasswordReminder_WhenProvidedWithEmailAndDomain_CallsExpectedUrl()
        {
            const string email = "some@email.com";
            const string domain = "www.tempori.org";
            
            var httpClient = new MockHttpClient<object>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, object>(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion){WhiteLabelDomain = domain}, httpClient);
            
            api.RequestPasswordReminder(email);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account/{3}/requestpasswordreminder?domain={4}", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, email, domain)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("GET"));
        }

        [Test]
        public void AreCredentialsValid_WhenProvidedWithEmailAndPassword_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<object>(HttpStatusCode.OK);
            var api = ApiClient.Create<AccountApi, object>(httpClient);
            const string email = "some@email.com";
            const string password = "password";

            api.AreCredentialsValid(email, password);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/account/validate", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("POST"));
        }

    }
}
