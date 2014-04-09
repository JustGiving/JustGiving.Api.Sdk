using System;
using System.IO;
using System.Text;
using GemBox.Spreadsheet;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    public class ApiTestFixture
    {
        protected static DataClientConfiguration GetDefaultDataClientConfiguration()
        {
            return new DataClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1)
                       {
                           WireDataFormat = WireDataFormat.Json,
                           IsZipSupportedByClient = false,
                           Username = TestContext.TestUsername,
                           Password = TestContext.TestValidPassword,
                           ConnectionTimeOut = TimeSpan.FromMinutes(20)
                       };
        }

        protected static void AssertResponseDoesNotHaveAnError(byte[] payment)
        {
            Assert.That(!Encoding.UTF8.GetString(payment).Contains("<error>"));
        }

        protected static void LoadDataInToWorkSheet(MemoryStream stream, ExcelFile sheet, DataFileFormat fileFormat)
        {
            if (fileFormat == DataFileFormat.excel)
            {
                sheet.LoadXls(stream);
                stream.Close();
                return;
            }

            File.WriteAllBytes(@"C:\Test.csv", stream.ToArray());
            sheet.LoadCsv(stream, CsvType.CommaDelimited);
            stream.Close();
        }
    }
}