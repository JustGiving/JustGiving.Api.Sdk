using JustGiving.Api.Data.Sdk.Model.Payment.Donations;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class PaymentsApiClient_DonationReportTests : ApiTestFixture
    {
        private const int BadPaymentId = -1;

        [Test]
        public void ResourceExists_ReturnsPayment_RawXml()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);
            
            var client = new JustGivingDataClient(clientConfiguration);           
            var payment = client.Payment.Report<Payment>(TestContext.KnownDonationPaymentId);

            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_ReturnsPayment_GZipXml()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml)
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);
            
            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payment.Report<Payment>(TestContext.KnownDonationPaymentId);
            
            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_ReturnsPayment_RawJson()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Json);               
            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payment.Report<Payment>(TestContext.KnownDonationPaymentId);

            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_ReturnsPayment_GZipJson()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Json)
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);

            var client = new JustGivingDataClient(clientConfiguration);

            var payment = client.Payment.Report<Payment>(TestContext.KnownDonationPaymentId);

            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceDoesNotExist_ThrowsNotFoundException()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Json)
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);

            var client = new JustGivingDataClient(clientConfiguration);
            
            Assert.Throws<ResourceNotFoundException>(() => client.Payment.Report<Payment>(BadPaymentId));
        }
    }
}
