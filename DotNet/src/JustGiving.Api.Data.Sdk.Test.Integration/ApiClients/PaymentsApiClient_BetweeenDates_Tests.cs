using System;
using System.Collections.Generic;
using System.Linq;
using JustGiving.Api.Data.Sdk.Model.Payment;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class PaymentsApiClient_BetweeenDates_Tests : ApiTestFixture
    {
        [Test]
        public void CanGetDataBetweenTwoDates()
        {
            var clientConfiguration = GetDataClientConfiguration();

            var dataClient = new JustGivingDataClient(clientConfiguration);
            var startDate = TestContext.StartDate; 
            var endDate = startDate.AddMonths(3);
            
            var response = dataClient.Payments.PaymentsBetween(startDate, endDate);
            
            Assert.IsNotNull(response);
            Assert.That(response.FirstOrDefault(i => i.PaymentDate > endDate), Is.Null);
            Assert.That(response.FirstOrDefault(i => i.PaymentDate < startDate), Is.Null);
        }

        [Test]
        public void CanProcessLotOfData()
        {
            var clientConfiguration = GetDataClientConfiguration();
            var dataClient = new JustGivingDataClient(clientConfiguration);
            var startDate = DateTime.Now.AddYears(-4);
            var endDate = startDate.AddMonths(3);

            var data = new List<PaymentSummary>();
            while(data.Count == 0 && endDate <= DateTime.Now)
            {
                data.AddRange(dataClient.Payments.PaymentsBetween(startDate, endDate));
                startDate = endDate.AddDays(1);
                endDate = startDate.AddMonths(3);
            }

            Assert.That(data.Count > 0);
        }


        [Test]
        public void DateRange_CannotExceedThreeMonths()
        {
            var clientConfiguration = GetDataClientConfiguration();
         var startDate = DateTime.Now.Date.AddYears(-2);
            var endDate = startDate.AddYears(1);
            var client = new JustGivingDataClient(clientConfiguration);
            var exception = Assert.Throws<ErrorResponseException>(() => client.Payments.PaymentsBetween(startDate, endDate));
            
            Assert.That(exception.Message.Contains("400"));
        }
    }
}