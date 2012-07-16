using System;
using System.IO;
using GemBox.Spreadsheet;
using GG.Api.Sdk;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slowest")]
    public class GetPageCreatedReportExcelDataTests
    {
        [Test]
        public void HasContent()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Other,
                                              IsZipSupportedByClient = false,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            // Act
            var data = dataApiClient.GetPagesCreated(startDate, endDate, DataFileFormat.excel);

            // Assert
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.GreaterThan(0));
        }

        [Test]
        public void HasContent_Gzip()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Other,
                                              IsZipSupportedByClient = true,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            // Act
            var data = dataApiClient.GetPagesCreated(startDate, endDate, DataFileFormat.excel);

            // Assert
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.GreaterThan(0));
        }

        [Test]
        public void IsValidExcelData()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Other,
                                              IsZipSupportedByClient = false,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            // Act
            var data = dataApiClient.GetPagesCreated(startDate, endDate, DataFileFormat.excel);

            // Assert
            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(data))
            {
                sheet.LoadXls(stream);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

        [Test]
        public void IsValidExcelData_Gzip()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Other,
                                              IsZipSupportedByClient = true,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PageCreatedReportClient(client);

            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            // Act
            var data = dataApiClient.GetPagesCreated(startDate, endDate, DataFileFormat.excel);

            // Assert
            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(data))
            {
                sheet.LoadXls(stream);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }
    }
}