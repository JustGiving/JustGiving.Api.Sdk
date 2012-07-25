using System;
using JustGiving.Api.Sdk;

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
    }
}