using System;
using GG.Api.Sdk;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class PaymentApiEndToEndTests
    {
        [Test]
        public void GetPaymentListAndDownloadSeveralPayments()
        {
            var config = ClientConfiguration.JsonClient()
                .Compressed()
                .AsUser(TestContext.TestUsername)
                .WithPassword(TestContext.TestValidPassword)
                .TimeoutAfter(minutes: 20);

            var client = new JustGivingClient(config);
            var paymentListClient = new PaymentListClient(client);
            var paymentReportClient = new PaymentReportClient(client);
                  
            int count = 0;
            const int numberToDownload = 10;
            var list = paymentListClient.GetPaymentSummaryList(DateTime.Now.AddMonths(-1), DateTime.Now);
            foreach(var payment in list)
            {
                if (count >= numberToDownload) break;
                paymentReportClient.GetDonationPaymentReport(payment.PaymentRef);
                count++;
            }
        }
    }
}