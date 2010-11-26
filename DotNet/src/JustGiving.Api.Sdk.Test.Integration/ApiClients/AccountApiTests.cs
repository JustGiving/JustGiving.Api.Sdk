using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Account;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class AccountApiTests : ApiClientTestsBase
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenSuppliedEmailIsUnused_AccountIsCreatedAndEmailAddressReturned(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);

            var registeredUsersEmail = accountClient.Create(request);

            Assert.AreEqual(email, registeredUsersEmail);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenSuppliedWithEmailThatIsAlreadyRegistered_ReturnsAnError(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);
            accountClient.Create(request);

            var exception = Assert.Throws<ErrorResponseException>(() => accountClient.Create(request));

            Assert.AreEqual(1, exception.Errors.Count);
            Assert.That(exception.Errors[0].Description, Is.StringContaining("email address in use"));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void ListAllPages_WhenSuppliedEmailIsValid_ListsPages(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client);

            var pages = accountClient.ListAllPages(TestContext.TestUsername);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client);

            var exists = accountClient.IsEmailRegistered("rasha@justgiving.com");

            Assert.IsTrue(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void IsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse(WireDataFormat format)
        {
            var client = CreateClientInvalidCredentials(format);
            var accountClient = new AccountApi(client);

            var exists = accountClient.IsEmailRegistered(Guid.NewGuid().ToString() + "@justgiving.com"); 

            Assert.IsFalse(exists);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Register_WhenSuppliedPasswordFormatInvalid_ReturnsAnError(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var accountClient = new AccountApi(client);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateInvalidPasswordFormatAccountRequest(email);
            
            var exception = Assert.Throws<ErrorResponseException>(() => accountClient.Create(request));

            Assert.AreEqual(1, exception.Errors.Count);
            Assert.That(exception.Errors[0].Description, Is.StringContaining("value provided is not valid for password"));
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

        public static CreateAccountRequest CreateInvalidPasswordFormatAccountRequest(string email)
        {
            var request = CreateValidRegisterAccountRequest(email);
            request.Password = TestContext.TestInvalidPasswordFormat;
            return request;
        }
    }
}
