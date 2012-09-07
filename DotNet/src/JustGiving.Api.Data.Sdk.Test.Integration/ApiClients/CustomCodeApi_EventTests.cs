using System;
using System.Net;
using System.Linq;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Model.CustomCodes;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class CustomCodeApi_EventTests : ApiTestFixture
    {
        private CustomCodesApi _customCodeClient;

        [SetUp]
        public void SetUp()
        {
            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            _customCodeClient = CreateCustomCodeClient(client);
        }

        [Test]
        public void CanSetCustomCode()
        {
            var response = _customCodeClient.SetEventCustomCodes(TestContext.KnownEventId, new EventCustomCodes { CustomCode1 = "foo" });

            Assert.That(response.Href, Is.Not.Empty);
        }

        [Test]
        public void CanGetCustomCode()
        {
            
            var val = Guid.NewGuid().ToString().Substring(0, 5);
            _customCodeClient.SetEventCustomCodes(TestContext.KnownEventId, new EventCustomCodes { CustomCode1 = val });
            var response = _customCodeClient.RetrieveEventCustomCodes(TestContext.KnownEventId);
            Assert.That(response.CustomCode1, Is.EqualTo(val));
        }

        [Test]
        public void CanSetMultipleCustomCodes()
        {
            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            var response = client.CustomCodes.SetEventCustomCodes(new[] { new EventCustomCodesListItem { EventId = TestContext.KnownEventId, CustomCode1 = "foo" }, new EventCustomCodesListItem { EventId = TestContext.KnownEventId + 1, CustomCode1 = "bar" } });
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CanSetMultipleCustomCodesWithCsvData()
        {
            var csvString = string.Format("EventId,CustomCode1,CustomCode2,CustomCode3\r\n{0},value1,value2,value3\r\n{1},value1,value2,value3",
                    TestContext.KnownEventId, TestContext.KnownEventId + 1);

            var response = _customCodeClient.SetEventCustomCodes(csvString);
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Single()
        {
            var excep = Assert.Throws<ErrorResponseException>(() => _customCodeClient.SetEventCustomCodes(TestContext.KnownEventId, new EventCustomCodes { CustomCode1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" }));
            Assert.That(excep.Message.Contains("400"));
        }

        [Test]
        public void CustomCodesAreValidated_Multiple()
        {
            var response = _customCodeClient.SetEventCustomCodes(new[] { new EventCustomCodesListItem { EventId = TestContext.KnownEventId, CustomCode1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" }, 
                new EventCustomCodesListItem { EventId = TestContext.KnownEventId + 1, CustomCode1 = "bar" } });
            
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Csv()
        {
            var csvString =
                string.Format("EventId,CustomCode1,CustomCode2,CustomCode3\r\n{0},value1,value2,value3\r\n{1},value1,value222222222222222222222222222222222222222,value3",TestContext.KnownEventId, TestContext.KnownEventId + 1);

            var response = _customCodeClient.SetEventCustomCodes(csvString);

            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        private static DataClientConfiguration XmlDataClientConfiguration()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);
            return clientConfiguration;
        }

    }
}
