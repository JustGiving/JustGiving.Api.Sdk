using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class DonationApiTests : ApiClientTestsBase
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void GetDonation_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var donationClient = new DonationApi(client);

            var status = donationClient.Retrieve(21303723);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void GetDonationStatus_WhenSuppliedWithKnownExistingDonationId_ReturnsDonationStatus(WireDataFormat format)
        {
            var client = CreateClientValidCredentials(format);
            var donationClient = new DonationApi(client);

            var status = donationClient.RetrieveStatus(21305000);
        }
    }
}
