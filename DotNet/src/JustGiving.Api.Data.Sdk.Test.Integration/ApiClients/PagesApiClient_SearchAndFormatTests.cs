using System.IO;
using GemBox.Spreadsheet;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Model;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slowest")]
    public class PagesApiClient_SearchAndFormatTests : ApiTestFixture
    {
        [TestCase(DataFileFormat.csv)]
        [TestCase(DataFileFormat.excel)]
        public void CustomCodes_Existing_IsValidForFormat(DataFileFormat fileFormat)
        {
            
            var clientConfiguration = OtherFormatDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            var pagesClient = CreatePagesClient(client);

            var data = pagesClient.Search(new PageCreatedSearchQuery { EventCustomCode1 = TestContext.KnownEventCustomCode1, EventCustomCode2 = TestContext.KnownEventCustomCode2, EventCustomCode3 = TestContext.KnownEventCustomCode3 }, TestContext.KnownStartDateForPageSearch, TestContext.KnownEndDateForPageSearch, fileFormat);

            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(data))
            {
                LoadDataInToWorkSheet(stream, sheet, fileFormat);
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

        private DataClientConfiguration OtherFormatDataClientConfiguration()
        {
            return GetDefaultDataClientConfiguration().With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);
        }
    }
}