using System.IO;
using GemBox.Spreadsheet;
using GG.Api.Sdk;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class GetPaymentByIdAsExcelDataTests
    {
        [Test]
        public void ResourceExists_ReturnsData()
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
            var payment = dataApiClient.GetPaymentReport(TestContext.KnownDonationPaymentId, DataFileFormat.excel);

            // Assert
            Assert.IsNotNull(payment);
        }

        [Test]
        public void ResourceExists_DataIsValidExcel()
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
            var payment = dataApiClient.GetPaymentReport(TestContext.KnownDonationPaymentId, DataFileFormat.excel);


            // Assert
            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(payment))
            {
                sheet.LoadXls(stream);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ResourceExists_DataIsValidExcel_Compressed()
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
            var payment = dataApiClient.GetPaymentReport(TestContext.KnownDonationPaymentId, DataFileFormat.excel);


            // Assert
            SpreadsheetInfo.SetLicense(TestContext.GemBoxSerial);
            var sheet = new ExcelFile();
            using (var stream = new MemoryStream(payment))
            {
                sheet.LoadXls(stream);
                stream.Close();
            }

            Assert.That(sheet.Worksheets.Count, Is.GreaterThan(0));
        }

    }
}