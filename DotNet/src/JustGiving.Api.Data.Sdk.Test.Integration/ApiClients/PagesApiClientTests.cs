using System;
using System.Linq;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slowest")]
    public class PagesApiClientTests : ApiTestFixture
    {

        private DateTime _startDate;
        private DateTime _endDate;

        [SetUp]
        public void SetUp()
        {
            _startDate  = TestContext.PageCreatedStartDate;
            _endDate = _startDate.AddMonths(2);
        }
    

        [Test]
        public void DateRange_CannotExceedThreeMonths()
        {
            // Arrange
            var clientConfiguration =  GetDefaultDataClientConfiguration();

            var client = new JustGivingDataClient(clientConfiguration);
            
            var excep = Assert.Throws<ErrorResponseException>(() => client.Pages.Created(new DateTime(2011,4,28), DateTime.Now));
            Assert.That(excep.Message.Contains("400"));
        }

        [Test]
        public void DateRange_HasContent()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration(); 

            var client = new JustGivingDataClient(clientConfiguration);
            
            var report = client.Pages.Created(_startDate, _endDate);

            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void DateRange_ContentIsWithinBounds()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            
            var report = client.Pages.Created(_startDate, _endDate);

            // Assert
            Assert.That(report.Pages.Count(p => p.CreatedDate < _startDate), Is.EqualTo(0));
            Assert.That(report.Pages.Count(p => p.CreatedDate > _endDate), Is.EqualTo(0));
        }
    }
}