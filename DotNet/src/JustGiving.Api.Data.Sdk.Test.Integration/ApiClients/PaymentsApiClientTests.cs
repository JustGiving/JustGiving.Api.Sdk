using System.IO;
using GemBox.Spreadsheet;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    public enum PaymentType
    {
        GiftAid, 
        Donation
    }

    [TestFixture, Category("Slow")]
    public class PaymentsApiClientTests : ApiTestFixture
    {
        [TestCase(PaymentType.Donation, DataFileFormat.csv)]
        [TestCase(PaymentType.GiftAid, DataFileFormat.csv)]
        [TestCase(PaymentType.Donation, DataFileFormat.excel)]
        [TestCase(PaymentType.GiftAid, DataFileFormat.excel)]
        public void When_GettingPaymentReportForKnownPaymentId_DataIsReturned(PaymentType paymentType, DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);

            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payment.ReportFor(GetPaymentId(paymentType), fileFormat);
            AssertResponseDoesNotHaveAnError(payment);
            Assert.IsNotNull(payment);
        }

        private int GetPaymentId(PaymentType paymentType)
        {
            if (paymentType == PaymentType.GiftAid)
                return TestContext.KnownGiftAidPaymentId;

            return TestContext.KnownDonationPaymentId;
        }

        [TestCase(PaymentType.Donation, DataFileFormat.csv)]
        [TestCase(PaymentType.GiftAid, DataFileFormat.csv)]
        [TestCase(PaymentType.Donation, DataFileFormat.excel)]
        [TestCase(PaymentType.GiftAid, DataFileFormat.excel)]
        public void When_GettingPaymentReportForKnownPaymentId_DataIsReturned_AndCanBeWrittenInValidFormat(PaymentType paymentType, DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                                        .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);

            var client = new JustGivingDataClient(clientConfiguration);
            var payment = client.Payment.ReportFor(GetPaymentId(paymentType), fileFormat);

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

        [TestCase(PaymentType.Donation, DataFileFormat.csv)]
        [TestCase(PaymentType.GiftAid, DataFileFormat.csv)]
        [TestCase(PaymentType.Donation, DataFileFormat.excel)]
        [TestCase(PaymentType.GiftAid, DataFileFormat.excel)]
        public void When_GettingPaymentReportForKnownPaymentId_DataIsReturned_AndCanBeWrittenInValidFormat_AndCompressed(PaymentType paymentType, DataFileFormat fileFormat)
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.IsZipSupportedByClient = true)
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Other);
                                        
            
            var client = new JustGivingDataClient(clientConfiguration);

            var payment = client.Payment.ReportFor(GetPaymentId(paymentType), fileFormat);

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
    }
}