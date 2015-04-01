using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    /// <summary>
    /// These tests aren't very good. Because we don't offer a full donation api, we can't populate donations for testing purposes
    /// Sorry!
    /// </summary>
    [TestFixture]
    public class DonationApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void GetDonation_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
            var donationClient = new DonationApi(client.HttpChannel);

            var status = donationClient.Retrieve(20905200);

            Assert.IsNotNull(status);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void GetDonationStatus_WhenSuppliedWithKnownExistingDonationId_ReturnsDonationStatus(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
            var donationClient = new DonationApi(client.HttpChannel);

            var status = donationClient.RetrieveStatus(20905200);

            Assert.IsNotNull(status);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void GetDonation_WhenSuppliedWithKnownExistingReference_ReturnsDonation(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var donationsResources = new DonationApi(client.HttpChannel);
            const string reference = "battlehack";

            //act
            var result = donationsResources.Retrieve(reference);

            //assert
            Assert.IsNotNull(result);
        }
    }
}
