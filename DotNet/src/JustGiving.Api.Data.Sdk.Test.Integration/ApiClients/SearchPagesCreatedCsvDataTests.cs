using System;
using System.IO;
using GemBox.Spreadsheet;
using GG.Api.Sdk;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slowest")]
    public class SearchPagesCreatedCsvDataTests
    {
        [Test]
        public void CustomCodes_Existing_IsValidCsv()
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

            // Act
            var data = dataApiClient.Search(new PageCreatedSearchQuery { EventCustomCode1 = TestContext.KnownEventCustomCode1, EventCustomCode2 = TestContext.KnownEventCustomCode2, EventCustomCode3 = TestContext.KnownEventCustomCode3 }, DateTime.Now.AddMonths(-3), DateTime.Now, DataFileFormat.csv);

            // Assert
            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(data))
            {
                sheet.LoadCsv(stream, CsvType.CommaDelimited);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }
    }
}