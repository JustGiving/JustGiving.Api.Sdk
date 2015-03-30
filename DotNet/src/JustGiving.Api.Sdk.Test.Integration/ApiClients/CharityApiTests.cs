using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Charity;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class CharityApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveCharity_IssuedWithKnownId_ReturnsCharity(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var charityClient = new CharityApi(client.HttpChannel);

            var item = charityClient.Retrieve(2050);

            Assert.IsNotNull(item);
            Assert.That(item.Name, Is.StringContaining("The Demo Charity"));
            Assert.That(item.SmsShortName, Is.StringMatching("Your Charity Campaign"));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void AuthenticateCharityUser_ValidUser_ReturnsIsValidAndCharityId(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var charityClient = new CharityApi(client.HttpChannel);
            var authenticateCharityUserRequest = new AuthenticateCharityUserRequest()
            {
                Username = TestContext.CharityTestUserName,
                Password = TestContext.CharityTestUserPassword,
                Pin = TestContext.CharityTestUserPin
            };
            var response = charityClient.Authenticate(authenticateCharityUserRequest);
            Assert.That(response.IsValid, Is.True);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveEvents_WhenDoesntSuppliedCredentials_ReturnsEvents(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var charityClient = new CharityApi(client.HttpChannel);
            var response = charityClient.RetrieveEvents(2357);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CharityId, Is.Not.EqualTo(0));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void CharityDonations_WhenDoesntSuppliedCredentials_ReturnsDonations(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var charityResources = new CharityApi(client.HttpChannel);
            const int charityId = 2050;
            //act
            var result = charityResources.CharityDonations(charityId);

            //assert
            CollectionAssert.IsNotEmpty(result.Donations);
        }
    }
}
