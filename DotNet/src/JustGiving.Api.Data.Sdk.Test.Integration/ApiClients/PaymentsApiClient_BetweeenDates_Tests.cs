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
            var clientConfiguration = GetDefaultDataClientConfiguration();

            var dataClient = new JustGivingDataClient(clientConfiguration);
            var startDate = TestContext.StartDate; 
            var endDate = startDate.AddMonths(3);
            
            var response = dataClient.Payment.PaymentsBetween(startDate, endDate);
            
            Assert.IsNotNull(response);
            Assert.That(response.Count(), Is.GreaterThan(0));
            Assert.That(response.FirstOrDefault(i => i.PaymentDate > endDate), Is.Null);
            Assert.That(response.FirstOrDefault(i => i.PaymentDate < startDate), Is.Null);
        }

        [Test]
        public void CanProcessLotOfData()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration();
            var dataClient = new JustGivingDataClient(clientConfiguration);
            var startDate = DateTime.Now.AddYears(-4);
            var endDate = startDate.AddMonths(3);

            var data = new List<PaymentSummary>();
            while(data.Count == 0 && startDate <= DateTime.Now.AddMonths(4))
            {
                var response = dataClient.Payment.PaymentsBetween(startDate, endDate); 
                if (response.Any())
                {
                    data.AddRange(response);
                    startDate = endDate.AddDays(1);
                    endDate = startDate.AddMonths(3);
                }
                
            }

            Assert.That(data.Count > 0);
        }


        [Test]
        public void DateRange_CannotExceedThreeMonths()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration();
         var startDate = DateTime.Now.Date.AddYears(-2);
            var endDate = startDate.AddYears(1);
            var client = new JustGivingDataClient(clientConfiguration);
            var exception = Assert.Throws<ErrorResponseException>(() => client.Payment.PaymentsBetween(startDate, endDate));
            
            Assert.That(exception.Message.Contains("400"));
        }
    }
}