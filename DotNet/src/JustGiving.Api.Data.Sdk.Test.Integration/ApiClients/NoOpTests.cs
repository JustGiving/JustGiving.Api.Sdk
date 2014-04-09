using System.Net;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class NoOpTests : ApiTestFixture
    {
        private DataClientConfiguration _dataClientConfiguration;
        private JustGivingDataClient _client;

        [Test]
        public void CanDoNoOp()
        {
            _dataClientConfiguration = GetDefaultDataClientConfiguration();
            _client = new JustGivingDataClient(_dataClientConfiguration);

            var response = _client.Payment.NoOp();
            Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ErrorIfAuthenticationFails()
        {
            _dataClientConfiguration = GetDefaultDataClientConfiguration().With((clientConfig) => clientConfig.Password = "NotARealPassword");
            _client = new JustGivingDataClient(_dataClientConfiguration);

            var response = _client.Payment.NoOp();
            
            Assert.That(response, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}