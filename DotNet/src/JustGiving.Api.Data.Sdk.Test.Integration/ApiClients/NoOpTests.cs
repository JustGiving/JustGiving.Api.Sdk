using System.Net;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class NoOpTests : ApiTestFixture
    {
        private DataClientConfiguration _dataClientConfiguration;
        private JustGivingDataClient _client;

        [SetUp]
        public void SetUp()
        {
            _dataClientConfiguration = GetDefaultDataClientConfiguration();
            _client = new JustGivingDataClient(_dataClientConfiguration);
        }

        [Test]
        public void CanDoNoOp()
        {
            var response = _client.Payment.NoOp();
            Assert.That(response, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ErrorIfAuthenticationFails()
        {
            var response = _client.Payment.NoOp();
            Assert.That(response, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}