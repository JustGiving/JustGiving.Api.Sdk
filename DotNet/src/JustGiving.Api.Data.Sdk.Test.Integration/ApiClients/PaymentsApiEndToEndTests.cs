using System;
using JustGiving.Api.Data.Sdk.Model.Payment.Donations;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class PaymentsApiEndToEndTests : ApiTestFixture
    {
        [Test]
        public void GetPaymentListAndDownloadSeveralPayments()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Json)
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);

            var client = new JustGivingDataClient(clientConfiguration);
            
            int count = 0;
            const int numberToDownload = 10;
            
            var payments = client.Payment.PaymentsBetween(DateTime.Now.AddMonths(-1), DateTime.Now);
            foreach(var payment in payments)
            {
                if (count >= numberToDownload) break;
                var report = client.Payment.Report<Payment>(payment.PaymentRef);

                Assert.That(report, Is.Not.Null);
                count++;
            }
        }
    }
}