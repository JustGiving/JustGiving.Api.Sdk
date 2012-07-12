using GG.Api.Sdk;
using GG.Api.Sdk.Http;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class GetPaymentDonationReportByIdWebFormatTests
    {
        private const int BadPaymentId = -1;

        [Test]
        public void ResourceExists_ReturnsPayment_RawXml()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Xml,
                                              IsZipSupportedByClient = false,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetDonationPaymentReport(TestContext.KnownDonationPaymentId);

            // Assert
            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_ReturnsPayment_GZipXml()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = true,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetDonationPaymentReport(TestContext.KnownDonationPaymentId);

            // Assert
            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_ReturnsPayment_RawJson()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Json,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetDonationPaymentReport(TestContext.KnownDonationPaymentId);

            // Assert
            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_ReturnsPayment_GZipJson()
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
            var payment = dataApiClient.GetDonationPaymentReport(TestContext.KnownDonationPaymentId);

            // Assert
            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceDoesNotExist_ThrowsNotFoundException()
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
            Assert.Throws<ResourceNotFoundException>(() => dataApiClient.GetDonationPaymentReport(BadPaymentId));
        }
    }
}
