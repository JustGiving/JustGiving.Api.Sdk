using System;
using System.Net;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Donation;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
    public class DonationApiTests
    {

        [Test]
        public void Create_WhenProvidedWithRequest_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<DonationStatus>(HttpStatusCode.OK);
            var api = CreateDonationApiClient(httpClient);

            api.RetrieveStatus(1);

            Assert.That(httpClient.LastRequest.Uri.ToString(), Is.StringContaining(string.Format("{0}{1}/v{2}/donation/{3}/status" , TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, 1)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("GET"));
        }

        private static DonationApi CreateDonationApiClient<T>(MockHttpClient<T> httpClient) where T : class, new()
        {
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion);
            var parent = new JustGivingClient(config, httpClient);
            return new DonationApi(parent);
        }
    }
}
