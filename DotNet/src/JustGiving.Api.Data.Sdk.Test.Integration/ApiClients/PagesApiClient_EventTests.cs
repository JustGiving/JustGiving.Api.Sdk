using System;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slowest")]
    public class PagesApiClient_EventTests : ApiTestFixture
    {
        [Test]
        public void DateRange_HasContent()
        {
            var clientConfiguration = GetDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);

            var client = new JustGivingDataClient(clientConfiguration);

            var startDate = new DateTime(2010, 02, 01);
            var endDate = startDate.AddMonths(2);

            var report = client.Pages.Created(startDate, endDate, TestContext.KnownEventIdWithPage);

            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        //[Test]
        //public void AllPagesBelongToEvent()
        //{

        //    var clientConfiguration = GetDataClientConfiguration()
        //        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml)
        //        .With((clientConfig) => clientConfig.IsZipSupportedByClient = false);

        //    var client = new JustGivingDataClient(clientConfiguration);
            
        //    var report = client.Pages.GetPagesCreatedForEvent(TestContext.KnownEventIdWithPage, new DateTime(2011,4,28), new DateTime(2011,6,28));

        //    // Assert
        //    Assert.That(report.Pages.Where(p => p.Event.Id == TestContext.KnownEventIdWithPage).Count(), Is.GreaterThan(0));
        //    Assert.That(report.Pages.Where(p => p.Event.Id != TestContext.KnownEventIdWithPage).Count(), Is.EqualTo(0));
        //}
//
//        [Test]
//        public void BadEventId_DateRange_Returns404()
//        {
//            // Arrange
//            var clientConfiguration = new ClientConfiguration
//            {
//                WireDataFormat = WireDataFormat.Xml,
//                IsZipSupportedByClient = false,
//                Username = NUnit.Framework.TestContext.TestUsername,
//                Password = NUnit.Framework.TestContext.TestValidPassword
//            };
//
//            var client = new JustGivingClient(clientConfiguration);
//            var dataApiClient = new PageCreatedReportClient(client);
//
//            // Act / Assert
//            Assert.Throws<ResourceNotFoundException>(() => dataApiClient.GetPagesCreatedForEvent(-1, DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(-10)));
//        }
    }
}