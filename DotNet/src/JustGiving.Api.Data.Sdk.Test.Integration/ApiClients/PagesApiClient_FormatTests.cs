using System;
using System.IO;
using GemBox.Spreadsheet;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slowest")]
    public class PagesApiClient_FormatTests : ApiTestFixture
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

        [TestCase(DataFileFormat.csv)]
        [TestCase(DataFileFormat.excel)]
        public void HasContent(DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                    .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other); 

            var client = new JustGivingDataClient(clientConfiguration);

            var data = client.Pages.Created(_startDate, _endDate, fileFormat);

            Assert.That(data, Is.Not.Null);
            AssertResponseDoesNotHaveAnError(data);
            Assert.That(data.Length, Is.GreaterThan(0));
        }

        [TestCase(DataFileFormat.csv)]
        [TestCase(DataFileFormat.excel)]
        public void HasContent_Gzip(DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                    .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other)
                    .With((clientConfig) => clientConfig.IsZipSupportedByClient = true);

            var data = GetPagesCreated(clientConfiguration, fileFormat);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.GreaterThan(0));
        }

        private byte[] GetPagesCreated(DataClientConfiguration clientConfiguration, DataFileFormat fileFormat)
        {
            var client = new JustGivingDataClient(clientConfiguration);
            return client.Pages.Created(_startDate, _endDate, fileFormat);
        }

        [TestCase(DataFileFormat.csv)]
        [TestCase(DataFileFormat.excel)]
        public void IsValidCsvData(DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other); 

            var client = new JustGivingDataClient(clientConfiguration);
            
            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            var data = client.Pages.Created(startDate, endDate, fileFormat);

            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            
            using (var stream = new MemoryStream(data))
            {
                LoadDataInToWorkSheet(stream, sheet, fileFormat);
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

        
        [TestCase(DataFileFormat.csv)]
        [TestCase(DataFileFormat.excel)]
        public void IsValidData_Gzip(DataFileFormat fileFormat)
        {

            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true)
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other); ;
            
            var client = new JustGivingDataClient(clientConfiguration);

            var startDate = new DateTime(2011,4,28);
            var endDate = new DateTime(2011,6,28);

            var data = client.Pages.Created(startDate, endDate, fileFormat);

            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(data))
            {
                LoadDataInToWorkSheet(stream, sheet, fileFormat);
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }
    }
}