using System.Net;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Donation;
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
            var api = ApiClient.Create<DonationApi, DonationStatus>(httpClient);

            api.RetrieveStatus(1);

            Assert.That(httpClient.LastRequestedUrl, Is.StringContaining(string.Format("{0}{1}/v{2}/donation/{3}/status" , TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, 1)));
            Assert.That(httpClient.LastRequest.Method, Is.StringContaining("GET"));
        }
    }
}
