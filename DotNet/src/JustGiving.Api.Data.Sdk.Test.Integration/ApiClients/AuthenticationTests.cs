using System.Net;
using GG.Api.Sdk;
using GG.Api.Sdk.Http;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class AuthenticationTests
    {
        [Test]
        public void AuthenticationSuccess_DoesNotReturnHttp401Unauthorised()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Json,
                                              IsZipSupportedByClient = true,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetDonationPaymentReport(1062979);

            // Assert
            Assert.That(payment.HttpStatusCode, Is.Not.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public void AuthenticationFailure_ReturnsHttp401Unauthorised()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Json,
                                              IsZipSupportedByClient = false,
                                              Username = "",
                                              Password = ""
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentReportClient(client);

            
            var exception =
                    Assert.Throws<ErrorResponseException>(() => dataApiClient.GetDonationPaymentReport(1062979));
            Assert.That(exception.Message.Contains("401"));
          
        }
    }
}