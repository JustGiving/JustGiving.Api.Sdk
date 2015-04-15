using System;
using System.Configuration;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Test.Integration.Configuration;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class AccountApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenSuppliedEmailIsUnused_AccountIsCreatedAndEmailAddressReturned(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);

            var registeredUsersEmail = accountClient.Create(request);

            Assert.AreEqual(email, registeredUsersEmail);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenSuppliedWithEmailThatIsAlreadyRegistered_ReturnsAnError(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);
            accountClient.Create(request);

            var exception = Assert.Throws<ErrorResponseException>(() => accountClient.Create(request));

            Assert.AreEqual(1, exception.Errors.Count);
            Assert.That(exception.Errors[0].Description, Is.StringContaining("email address is already in use"));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ListAllPages_WhenSuppliedEmailIsValid_ListsPages(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            accountClient.ListAllPages("apiunittest@justgiving.com");
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            var exists = accountClient.IsEmailRegistered(TestContext.TestUsername);

            Assert.IsTrue(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            var exists = accountClient.IsEmailRegistered(Guid.NewGuid().ToString() + "@justgiving.com");

            Assert.IsFalse(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenSuppliedPasswordFormatInvalid_ReturnsAnError(WireDataFormat format)
        {
            const string invalidPassowordValue = "abc"; //Password to short
            var client = TestContext.CreateClientNoCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);
            request.Password = invalidPassowordValue;

            var exception = Assert.Throws<ErrorResponseException>(() => accountClient.Create(request));

            Assert.AreEqual(1, exception.Errors.Count);
            Assert.That(exception.Errors[0].Description, Is.StringContaining("value provided is not valid for password"));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RequestPassWordReminder_WhenSuppliedKnownEmail_ReturnsTrue(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            accountClient.RequestPasswordReminder(TestContext.TestUsername);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RequestPassWordReminder_WhenSuppliedKnownEmailAndDomain_ReturnsTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            //act
            accountClient.RequestPasswordReminder(TestContext.TestUsername);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RequestRetrieveAccount_ReturnsAccountDetails(WireDataFormat format)
        {
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            var account = accountClient.RetrieveAccount();

            Assert.AreEqual(TestContext.TestUsername, account.Email);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ChangePassword_WhenSuppliedValidChangePasswordRequest_ReturnTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var validRequest = CreateValidChangePasswordForGivenAccount(TestContext.TestUsername);
            var accountClient = new AccountApi(client.HttpChannel);

            //act
            var result = accountClient.ChangePassword(validRequest);

            //assert
            Assert.IsTrue(result);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ContentRatingHistory_WhenSuppliedValidCredentials_ReturnContentRatingHistory(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            //act
            var result = accountClient.ContentRatingHistory();

            //assert
            Assert.IsNotNull(result);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RateContent_WhenValidRequest_ReturnTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountclient = new AccountApi(client.HttpChannel);
            var validRequest = CreateValidRateContentRequest();

            //act
            var response = accountclient.RateContent(validRequest);

            //assert
            Assert.IsTrue(response);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ContentFeed_WhenSuppliedValidCredentials_ReturnContentFeed(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            //act
            var result = accountClient.ContentFeed();

            //assert
            Assert.IsNotNull(result);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Interest_WhenSuppliedValidCredentials_ReturnListOfInterest(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);

            //act
            var result = accountClient.Interest();

            //assert
            Assert.IsNotNull(result);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void AddInterest_WhenSuppliedValidCredentialsAndValidRequest_ReturnTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);
            var validRequest = CreateValidUserInterestRequest();

            //act
            var result = accountClient.AddInterest(validRequest);

            //assert
            Assert.IsTrue(result);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ReplaceInterest_WhenSuppliedValidCredentialsAndValidRequest_REturnTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client.HttpChannel);
            var validRequest = CreateValidReplaceInterestRequest();

            //act
            var result = accountClient.ReplaceInterest(validRequest);

            //assert
            Assert.IsTrue(result);
        }

        private static AccountApi.ReplaceInterestRequest CreateValidReplaceInterestRequest()
        {
            return new AccountApi.ReplaceInterestRequest()
                {
                    "swimming" + Guid.NewGuid()
                };
        }

        private static AccountApi.UserInterest CreateValidUserInterestRequest()
        {
            return new AccountApi.UserInterest()
                {
                    Interest = "Foodball"
                };
        }

        private static AccountApi.RateContentRequest CreateValidRateContentRequest()
        {
            return new AccountApi.RateContentRequest
                {
                    ContentData = "my-page-short-url",
                    Intent = "Like",
                    Type = "FundraisingPage"
                };
        }

        private static CreateAccountRequest CreateValidRegisterAccountRequest(string email)
        {
            return new CreateAccountRequest
            {
                Email = email,
                FirstName = "Test",
                LastName = "Test",
                Password = TestContext.TestValidPassword,
                Title = "Mr",
                Address =
                {
                    Line1 = "line1",
                    Line2 = "line2",
                    Country = "England",
                    CountyOrState = "London",
                    PostcodeOrZipcode = "sk8 5hy",
                    TownOrCity = "London"
                },
                AcceptTermsAndConditions = true
            };
        }

        private static AccountApi.ChangePasswordRequest CreateValidChangePasswordForGivenAccount(string email)
        {
            return new AccountApi.ChangePasswordRequest
                {
                    CurrentPassword = "password",
                    EmailAddress = email,
                    NewPassword = "password"
                };
        }
    }
}
