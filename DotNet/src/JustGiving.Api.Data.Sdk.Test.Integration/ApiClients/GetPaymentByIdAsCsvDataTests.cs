using System.IO;
using GemBox.Spreadsheet;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class GetPaymentByIdAsCsvDataTests : ApiTestFixture
    {
        [TestCase(TestContext.KnownDonationPaymentId)]
        [TestCase(TestContext.KnownGiftAidPaymentId)]
        public void ResourceExists_ReturnsData(int paymentId)
        {
            var clientConfiguration = GetDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);


            var client = new JustGivingDataClient(clientConfiguration);

            var payment = client.Payments.ReportFor(paymentId, DataFileFormat.csv);

            Assert.IsNotNull(payment);
        }

        [TestCase(TestContext.KnownDonationPaymentId)]
        [TestCase(TestContext.KnownGiftAidPaymentId)]
        public void ResourceExists_DataIsValidCsv(int paymentId)
        {
            var clientConfiguration = GetDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);

            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payments.ReportFor(paymentId, DataFileFormat.csv);

            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(payment))
            {
                sheet.LoadCsv(stream, CsvType.CommaDelimited);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }


        [TestCase(TestContext.KnownDonationPaymentId)]
        [TestCase(TestContext.KnownGiftAidPaymentId)]
        public void ResourceExists_DataIsValidCsv_Compressed(int paymentId)
        {
            var clientConfiguration = GetDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.IsZipSupportedByClient = true)
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);

            
            var client = new JustGivingDataClient(clientConfiguration);
            
            var payment = client.Payments.ReportFor(paymentId, DataFileFormat.csv);
            
            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(payment))
            {
                sheet.LoadCsv(stream, CsvType.CommaDelimited);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

    }
}