using System.IO;
using GemBox.Spreadsheet;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class PaymentsApiClientTests : ApiTestFixture
    {
        [TestCase(TestContext.KnownDonationPaymentId, DataFileFormat.csv)]
        [TestCase(TestContext.KnownGiftAidPaymentId, DataFileFormat.csv)]
        [TestCase(TestContext.KnownDonationPaymentId, DataFileFormat.excel)]
        [TestCase(TestContext.KnownGiftAidPaymentId, DataFileFormat.excel)]
        public void When_GettingPaymentReportForKnownPaymentId_DataIsReturned(int paymentId, DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);

            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payment.ReportFor(paymentId, fileFormat);
            AssertResponseDoesNotHaveAnError(payment);
            Assert.IsNotNull(payment);
        }

        [TestCase(TestContext.KnownDonationPaymentId, DataFileFormat.csv)]
        [TestCase(TestContext.KnownGiftAidPaymentId, DataFileFormat.csv)]
        [TestCase(TestContext.KnownDonationPaymentId, DataFileFormat.excel)]
        [TestCase(TestContext.KnownGiftAidPaymentId, DataFileFormat.excel)]
        public void When_GettingPaymentReportForKnownPaymentId_DataIsReturned_AndCanBeWrittenInValidFormat(int paymentId, DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);

            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payment.ReportFor(paymentId, fileFormat);

            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(payment))
            {
                sheet.LoadCsv(stream, CsvType.CommaDelimited);
                stream.Close();
            }

            AssertResponseDoesNotHaveAnError(payment);
            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

        [TestCase(TestContext.KnownDonationPaymentId, DataFileFormat.csv)]
        [TestCase(TestContext.KnownGiftAidPaymentId, DataFileFormat.csv)]
        [TestCase(TestContext.KnownDonationPaymentId, DataFileFormat.excel)]
        [TestCase(TestContext.KnownGiftAidPaymentId, DataFileFormat.excel)]
        public void When_GettingPaymentReportForKnownPaymentId_DataIsReturned_AndCanBeWrittenInValidFormat_AndCompressed(int paymentId, DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDataClientConfiguration()
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true)
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);
                                        
            
            var client = new JustGivingDataClient(clientConfiguration);

            var payment = client.Payment.ReportFor(paymentId, fileFormat);

            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(payment))
            {
                LoadDataInToWorkSheet(stream, sheet, fileFormat);
                stream.Close();
            }

            AssertResponseDoesNotHaveAnError(payment);
            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

        private static void LoadDataInToWorkSheet(MemoryStream stream, ExcelFile sheet, DataFileFormat fileFormat)
        {
            if (fileFormat == DataFileFormat.excel)
            {
                sheet.LoadXls(stream);
                stream.Close();
                return;
            }
            
            sheet.LoadCsv(stream, CsvType.CommaDelimited);
            stream.Close();
        }
    }
}