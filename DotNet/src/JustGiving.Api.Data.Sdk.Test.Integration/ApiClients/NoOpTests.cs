using System;
using System.Net;
using GG.Api.Sdk;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture]
    public class NoOpTests
    {
        [Test]
        public void CanDoNoOp()
        {
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Json,
                                              IsZipSupportedByClient = false,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword,
                                              ConnectionTimeOut = TimeSpan.FromMinutes(20)
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentListClient(client);
            var response = dataApiClient.NoOp();
            Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ErrorIfAuthenticationFails()
        {
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Json,
                                              IsZipSupportedByClient = false,
                                              Username = TestContext.TestUsername,
                                              Password = string.Empty,
                                              ConnectionTimeOut = TimeSpan.FromMinutes(20)
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var dataApiClient = new PaymentListClient(client);
            var response = dataApiClient.NoOp();
            Assert.That(response, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}