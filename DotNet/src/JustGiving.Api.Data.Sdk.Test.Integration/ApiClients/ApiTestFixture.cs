using System;
using System.Text;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    public class ApiTestFixture
    {
        protected static DataClientConfiguration GetDataClientConfiguration()
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
    }
}