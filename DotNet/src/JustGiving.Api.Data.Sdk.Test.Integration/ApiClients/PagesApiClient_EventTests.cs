using System;
using System.Linq;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slowest")]
    public class PagesApiClient_EventTests : ApiTestFixture
    {
        private DateTime _startDate;
        private DateTime _endDate;

        [SetUp]
        public void SetUp()
        {
            _startDate = TestContext.PageCreatedStartDate;
            _endDate = _startDate.AddMonths(2);
        }
    
        [Test]
        public void DateRange_HasContent()
        {
            var clientConfiguration = DefaultClientConfiguration();

            var client = new JustGivingDataClient(clientConfiguration);
            var pagesClient = CreatePagesClient(client);
            var report = pagesClient.RetrievePagesCreated(_startDate, _endDate, TestContext.KnownEventIdWithPage);

            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void AllPagesBelongToEvent()
        {
            var clientConfiguration = DefaultClientConfiguration();

            var client = new JustGivingDataClient(clientConfiguration);
            var pagesClient = CreatePagesClient(client);
            var report = pagesClient.RetrievePagesCreated(_startDate, _endDate, TestContext.KnownEventIdWithPage);

            Assert.That(report.Pages.Count(p => p.Event.Id == TestContext.KnownEventIdWithPage), Is.GreaterThan(0));
            Assert.That(report.Pages.Count(p => p.Event.Id != TestContext.KnownEventIdWithPage), Is.EqualTo(0));
        }

        [Test]
        public void BadEventId_DateRange_Returns404()
        {
            var clientConfiguration = DefaultClientConfiguration();
            
            var client = new JustGivingDataClient(clientConfiguration);
            var pagesClient = CreatePagesClient(client);
            Assert.Throws<ResourceNotFoundException>(() => pagesClient.RetrievePagesCreated(DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(-10), -1));
        }

        private static DataClientConfiguration DefaultClientConfiguration()
        {
            return GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);
        }
    }
}