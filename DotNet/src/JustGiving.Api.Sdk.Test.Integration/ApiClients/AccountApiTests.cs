using System;
using System.Configuration;
using System.IO;
using System.Reflection;
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

            accountClient.ListAllPages(TestContext.TestUsername);
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

            var testConfigurations = (ITestConfigurations)ConfigurationManager.GetSection("testConfigurations"); 

            var client = TestContext.CreateClientInvalidCredentials(format);
            client.SetWhiteLabelDomain(testConfigurations.RflDomain);
			var accountClient = new AccountApi(client.HttpChannel);

            accountClient.RequestPasswordReminder(TestContext.TestUsername);
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
    }
}
