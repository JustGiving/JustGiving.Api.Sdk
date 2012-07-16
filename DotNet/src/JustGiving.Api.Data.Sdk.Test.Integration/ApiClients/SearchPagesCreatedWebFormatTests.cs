using System;
using GG.Api.Sdk;
using GG.Api.Services.Data.Dto.CustomCodes;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slowest")]
    public class SearchPagesCreatedWebFormatTests
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
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
            var dataApiClient = new CustomCodesClient(client);

            dataApiClient.SetPageCustomCodes(TestContext.KnownPageId, new PageCustomCodes
                                                                          {
                                                                              CustomCode1 =
                                                                                  TestContext.KnownPageCustomCode1,
                                                                              CustomCode2 =
                                                                                  TestContext.KnownPageCustomCode2,
                                                                              CustomCode3 =
                                                                                  TestContext.KnownPageCustomCode3,
                                                                              CustomCode4 =
                                                                                  TestContext.KnownPageCustomCode4,
                                                                              CustomCode5 =
                                                                                  TestContext.KnownPageCustomCode5,
                                                                              CustomCode6 =
                                                                                  TestContext.KnownPageCustomCode6
                                                                          });


        }

        [TestCase(TestContext.KnownEventCustomCode1, "", "")]
        [TestCase("", TestContext.KnownEventCustomCode2, "")]
        [TestCase("", "", TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, "", TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, TestContext.KnownEventCustomCode2, "")]
        [TestCase("", TestContext.KnownEventCustomCode2, TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, TestContext.KnownEventCustomCode2, TestContext.KnownEventCustomCode3)]
        public void EventCustomCodes_Existing_ReturnsResult(string customCode1, string customCode2, string customCode3)
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { EventCustomCode1 = customCode1, EventCustomCode2 = customCode2, EventCustomCode3 = customCode3 }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [TestCase(TestContext.KnownPageCustomCode1, "","","","","")]
        [TestCase("", TestContext.KnownPageCustomCode2, "", "", "", "")]
        [TestCase("", "", TestContext.KnownPageCustomCode3, "", "", "")]
        [TestCase("", "", "", TestContext.KnownPageCustomCode4, "", "")]
        [TestCase("", "", "", "", TestContext.KnownPageCustomCode5, "")]
        [TestCase("", "", "", "", "", TestContext.KnownPageCustomCode6)]
        [TestCase(TestContext.KnownPageCustomCode1, TestContext.KnownPageCustomCode2, TestContext.KnownPageCustomCode3, TestContext.KnownPageCustomCode4, TestContext.KnownPageCustomCode5, TestContext.KnownPageCustomCode6)]
        public void PageCustomCodes_Existing_ReturnsResult(string pageCustomCode1, string pageCustomCode2, string pageCustomCode3, string pageCustomCode4, string pageCustomCode5, string pageCustomCode6)
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = pageCustomCode1, PageCustomCode2 = pageCustomCode2, PageCustomCode3 = pageCustomCode3, PageCustomCode4 = pageCustomCode4, PageCustomCode5 = pageCustomCode5, PageCustomCode6 = pageCustomCode6 }, new DateTime(2006,1,1), new DateTime(2006,3,1));

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void FilterByStatus_IncludesExpectedResult()
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = TestContext.KnownPageCustomCode1, IsActivePage = false }, new DateTime(2006,1,1), new DateTime(2006,3,1));

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void FilterByStatus_ExcludesExpectedResult()
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = TestContext.KnownPageCustomCode1, IsActivePage = true }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterByAppeal_IncludesExpectedResult()
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = TestContext.KnownPageCustomCode1, AppealName = TestContext.KnownAppealName }, new DateTime(2006,1,1), new DateTime(2006,3,1));

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [Test]
        public void FilterByAppeal_ExcludesExpectedResult()
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = TestContext.KnownPageCustomCode1, AppealName = "flurble" }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterByExpiresAfterDate_ExcludesExpectedResult()
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = TestContext.KnownPageCustomCode1, PageExpiresAfter = TestContext.KnownExpiryDate.AddDays(1) }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterByExpiresBeforeDate_ExcludesExpectedResult()
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { PageCustomCode1 = TestContext.KnownPageCustomCode1, PageExpiresBefore = TestContext.KnownExpiryDate.AddDays(-1) }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterByExpiryDate_IncludesExpectedResult()
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
            var report =
                dataApiClient.Search(new PageCreatedSearchQuery
                                         {
                                             PageCustomCode1 = TestContext.KnownPageCustomCode1,
                                             PageExpiresAfter = TestContext.KnownExpiryDate,
                                             PageExpiresBefore = TestContext.KnownExpiryDate
                                         }, new DateTime(2006,1,1), new DateTime(2006,3,1));

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [TestCase(TestContext.KnownEventCustomCode1, "", "")]
        [TestCase("", TestContext.KnownEventCustomCode2, "")]
        [TestCase("", "", TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, "", TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, TestContext.KnownEventCustomCode2, "")]
        [TestCase("", TestContext.KnownEventCustomCode2, TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, TestContext.KnownEventCustomCode2, TestContext.KnownEventCustomCode3)]
        public void EventCustomCodes_Existing_WithDates_ReturnsResult(string customCode1, string customCode2, string customCode3)
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
            var startDate = new DateTime(2004, 1, 1);
            var endDate = new DateTime(2004, 2, 1);

            // Act
            var report = dataApiClient.Search(new PageCreatedSearchQuery { EventCustomCode1 = customCode1, EventCustomCode2 = customCode2, EventCustomCode3 = customCode3 }, startDate, endDate);

            // Assert
            Assert.That(report.Pages.Count, Is.GreaterThan(0));
        }

        [TestCase("QWERTYUIOP", "", "")]
        [TestCase("", "QWERTYUIOP", "")]
        [TestCase("", "", "QWERTYUIOP")]
        public void EventCustomCodes_NotExisting_NoResult(string customCode1, string customCode2, string customCode3)
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { EventCustomCode1 = customCode1, EventCustomCode2 = customCode2, EventCustomCode3 = customCode3 }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.EqualTo(0));
        }

        [TestCase("QWERTYUIOP", TestContext.KnownEventCustomCode2, "")]
        [TestCase("QWERTYUIOP", "", TestContext.KnownEventCustomCode3)]
        [TestCase(TestContext.KnownEventCustomCode1, "QWERTYUIOP", TestContext.KnownEventCustomCode3)]
        public void EventCustomCodes_SomeExistingSomeNot_NoResult(string customCode1, string customCode2, string customCode3)
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
            var report = dataApiClient.Search(new PageCreatedSearchQuery { EventCustomCode1 = customCode1, EventCustomCode2 = customCode2, EventCustomCode3 = customCode3 }, new DateTime(2004,1,1), new DateTime(2004,2,1));

            // Assert
            Assert.That(report.Pages.Count, Is.EqualTo(0));
        }        
    }
}
