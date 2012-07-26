using JustGiving.Api.Data.Sdk.Model.Payment;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class GetPaymentGiftAidReportByIdWebFormatTests : ApiTestFixture
    {
        private const int BadPaymentId = -1;

        [Test]
        public void ResourceExists_ReturnsPayment_RawXml()
        {
            var clientConfiguration = GetDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);
            
            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payments.Report<Payment>(TestContext.KnownGiftAidPaymentId);
            // Act
            //var payment = client.GetGiftAidPaymentReport(TestContext.KnownGiftAidPaymentId);

            // Assert
            Assert.IsNotNull(payment);
        }

//        [Test]
//        public void ResourceExists_ReturnsPayment_GZipXml()
//        {
//            // Arrange
//            var clientConfiguration = new ClientConfiguration
//            {
//                WireDataFormat = WireDataFormat.Xml,
//                IsZipSupportedByClient = true,
//                Username = TestContext.TestUsername,
//                Password = TestContext.TestValidPassword
//            };
//
//            var client = new JustGivingClient(clientConfiguration);
//            var dataApiClient = new PaymentReportClient(client);
//
//            // Act
//            var payment = dataApiClient.GetGiftAidPaymentReport(TestContext.KnownGiftAidPaymentId);
//
//            // Assert
//            Assert.IsNotNull(payment);
//        }
//
//        [Test]
//        public void ResourceExists_ReturnsPayment_RawJson()
//        {
//            // Arrange
//            var clientConfiguration = new ClientConfiguration
//            {
//                WireDataFormat = WireDataFormat.Json,
//                IsZipSupportedByClient = false,
//                Username = TestContext.TestUsername,
//                Password = TestContext.TestValidPassword
//            };
//
//            var client = new JustGivingClient(clientConfiguration);
//            var dataApiClient = new PaymentReportClient(client);
//
//            // Act
//            var payment = dataApiClient.GetGiftAidPaymentReport(TestContext.KnownGiftAidPaymentId);
//
//            // Assert
//            Assert.IsNotNull(payment);
//        }
//
//        [Test]
//        public void ResourceExists_ReturnsPayment_GZipJson()
//        {
//            // Arrange
//            var clientConfiguration = new ClientConfiguration
//            {
//                WireDataFormat = WireDataFormat.Json,
//                IsZipSupportedByClient = true,
//                Username = TestContext.TestUsername,
//                Password = TestContext.TestValidPassword
//            };
//
//            var client = new JustGivingClient(clientConfiguration);
//            var dataApiClient = new PaymentReportClient(client);
//
//            // Act
//            var payment = dataApiClient.GetGiftAidPaymentReport(TestContext.KnownGiftAidPaymentId);
//
//            // Assert
//            Assert.IsNotNull(payment);
//        }
//
//        [Test]
//        public void ResourceDoesNotExist_ThrowsNotFoundException()
//        {
//            // Arrange
//            var clientConfiguration = new ClientConfiguration
//            {
//                WireDataFormat = WireDataFormat.Json,
//                IsZipSupportedByClient = true,
//                Username = TestContext.TestUsername,
//                Password = TestContext.TestValidPassword
//            };
//
//            var client = new JustGivingClient(clientConfiguration);
//            var dataApiClient = new PaymentReportClient(client);
//
//            // Act
//            Assert.Throws<ResourceNotFoundException>(() => dataApiClient.GetGiftAidPaymentReport(BadPaymentId));
//        }
    }
}
