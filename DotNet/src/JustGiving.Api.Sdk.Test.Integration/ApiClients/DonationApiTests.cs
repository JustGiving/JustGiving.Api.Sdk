using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class DonationApiTests
    {
        [Test]
        public void GetDonationStatus_WhenSuppliedWithKnownExistingDonationId_ReturnsDonationStatus()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", Password = "incorrectPassword" });
            var donationClient = new DonationApi(client);

            var status = donationClient.RetrieveStatus(21305000);
        }
    }
}
