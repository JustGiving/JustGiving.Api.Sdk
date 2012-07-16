using System;
using System.Linq;
using GG.Api.Sdk;
using GG.Api.Sdk.Http;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slowest")]
    public class GetPageCreatedForEventReportWebFormatTests
    {
        [Test]
        public void DateRange_HasContent()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);
            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            // Act
            var report = dataApiClient.GetPagesCreatedForEvent(TestContext.KnownEventIdWithPage, startDate, endDate);

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void AllPagesBelongToEvent()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            // Act
            var report = dataApiClient.GetPagesCreatedForEvent(TestContext.KnownEventIdWithPage, new DateTime(2011,4,28), new DateTime(2011,6,28));

            // Assert
            Assert.That(report.Pages.Where(p => p.Event.Id == TestContext.KnownEventIdWithPage).Count(), Is.GreaterThan(0));
            Assert.That(report.Pages.Where(p => p.Event.Id != TestContext.KnownEventIdWithPage).Count(), Is.EqualTo(0));
        }

        [Test]
        public void BadEventId_DateRange_Returns404()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            // Act / Assert
            Assert.Throws<ResourceNotFoundException>(() => dataApiClient.GetPagesCreatedForEvent(-1, DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(-10)));
        }
    }
}