using JustGiving.Api.Data.Sdk.Model.Payment;
using JustGiving.Api.Data.Sdk.Model.Payment.GiftAid;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class PaymentsApiClient_GiftAidReportTests : ApiTestFixture
    {
        private const int BadPaymentId = -1;
        
        [TestCase(WireDataFormat.Json, true)]
        [TestCase(WireDataFormat.Json, false)]
        [TestCase(WireDataFormat.Xml, true)]
        [TestCase(WireDataFormat.Xml, false)]
        public void ResourceExists_ReturnsPayment(WireDataFormat format, bool isZipSupported)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
               .With((clientConfig) => clientConfig.WireDataFormat = format)
               .With((clientConfig) => clientConfig.IsZipSupportedByClient = isZipSupported);

            Assert.IsNotNull(GetPayment(clientConfiguration));
        }

        [Test]
        public void ResourceDoesNotExist_ThrowsNotFoundException()
        {

            var clientConfiguration = GetDefaultDataClientConfiguration()
                    .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Json)
                    .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);
            
            Assert.Throws<ResourceNotFoundException>(() => GetPayment(clientConfiguration, BadPaymentId));
        }

        private Payment GetPayment(DataClientConfiguration clientConfiguration, int paymentId = 0)
        {
            var client = new JustGivingDataClient(clientConfiguration);
            CreatePaymentsClient(client);
            var payment = PaymentsClient.RetrieveReport<Payment>(paymentId == 0 ? TestContext.KnownGiftAidPaymentId : paymentId);
            return payment;
        }

    }
}
