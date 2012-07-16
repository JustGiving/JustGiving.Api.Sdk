using System;
using System.Linq;
using GG.Api.Sdk;
using GG.Api.Sdk.Http;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slowest")]
    public class GetPageCreatedReportWebFormatTests
    {
        [Test]
        public void DateRange_CannotExceedThreeMonths()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Json,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            // Act
            var excep = Assert.Throws<ErrorResponseException>(() => dataApiClient.GetPagesCreated(new DateTime(2011,4,28), DateTime.Now));

            // Assert
            Assert.That(excep.Message.Contains("400"));
        }

        [Test]
        public void DateRange_HasContent()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Json,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            // Act
            var report = dataApiClient.GetPagesCreated(new DateTime(2011,4,28), new DateTime(2011,6,28));

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void DateRange_ContentIsWithinBounds()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Json,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            // Act
            var report = dataApiClient.GetPagesCreated(startDate, endDate);

            // Assert
            Assert.That(report.Pages.Where(p => p.CreatedDate < startDate).Count(), Is.EqualTo(0));
            Assert.That(report.Pages.Where(p => p.CreatedDate > endDate).Count(), Is.EqualTo(0));
        }
    }
}