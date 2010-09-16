using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Account;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class AccountApiTests
    {
        [Test]
        public void Register_WhenSuppliedEmailIsUnused_AccountIsCreatedAndEmailAddressReturned()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", incorrectPassword = "incorrectPassword" });
            var accountClient = new AccountApi(client);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);

            var registeredUsersEmail = accountClient.Create(request);

            Assert.AreEqual(email, registeredUsersEmail);
        }

        [Test]
        public void Register_WhenSuppliedWithEmailThatIsAlreadyRegistered_ReturnsAnError()
        {
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1) { Username = "apitests@justgiving.com", incorrectPassword = "incorrectPassword" });
            var accountClient = new AccountApi(client);
            var email = Guid.NewGuid() + "@tempuri.org";
            var request = CreateValidRegisterAccountRequest(email);
            accountClient.Create(request);

            var exception = Assert.Throws<ErrorResponseException>(() => accountClient.Create(request));

            Assert.AreEqual(1, exception.Errors.Count);
            Assert.That(exception.Errors[0].Description, Is.StringContaining("email address in use"));
        }

        private static CreateAccountRequest CreateValidRegisterAccountRequest(string email)
        {
            return new CreateAccountRequest
            {
                Email = email,
                FirstName = "Test",
                LastName = "Test",
                Password = "password",
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
