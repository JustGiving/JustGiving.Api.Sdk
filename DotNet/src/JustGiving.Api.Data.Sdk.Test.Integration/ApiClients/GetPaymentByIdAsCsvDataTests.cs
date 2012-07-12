using System.IO;
using GemBox.Spreadsheet;
using GG.Api.Sdk;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class GetPaymentByIdAsCsvDataTests
    {
        [TestCase(TestContext.KnownDonationPaymentId)]
        [TestCase(TestContext.KnownGiftAidPaymentId)]
        public void ResourceExists_ReturnsData(int paymentId)
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
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetPaymentReport(paymentId, DataFileFormat.csv);

            // Assert
            Assert.IsNotNull(payment);
        }

        [TestCase(TestContext.KnownDonationPaymentId)]
        [TestCase(TestContext.KnownGiftAidPaymentId)]
        public void ResourceExists_DataIsValidCsv(int paymentId)
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
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetPaymentReport(paymentId, DataFileFormat.csv);


            // Assert
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
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Other,
                IsZipSupportedByClient = true,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword
            };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentReportClient(client);

            // Act
            var payment = dataApiClient.GetPaymentReport(paymentId, DataFileFormat.csv);


            // Assert
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