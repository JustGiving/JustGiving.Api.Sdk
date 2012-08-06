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
    public class CustomCodeApi_PageTests : ApiTestFixture
    {
        private DataClientConfiguration _dataClientConfiguration;
        private CustomCodesApi _customCodeClient;

        [SetUp]
        public void SetUp()
        {
            _dataClientConfiguration = XmlDataConfiguration();
            var client = new JustGivingDataClient(_dataClientConfiguration);
            _customCodeClient = CreateCustomCodeClient(client);

        }

        [Test]
        public void CanSetCustomCode()
        {
            var response = _customCodeClient.SetPageCustomCodes(TestContext.KnownPageIdWithCustomCodes, new PageCustomCodes { CustomCode1 = "foo" });
            Assert.That(response.HttpStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        
        [Test]
        public void CanGetCustomCode()
        {
            var val = Guid.NewGuid().ToString().Substring(0, 5);
            _customCodeClient.SetPageCustomCodes(TestContext.KnownPageIdWithCustomCodes, new PageCustomCodes { CustomCode1 = val });

            var response = _customCodeClient.RetrievePageCustomCodes(TestContext.KnownPageIdWithCustomCodes);
            Assert.That(response.CustomCode1, Is.EqualTo(val));
        }

        [Test]
        public void CanSetMultipleCustomCodes()
        {
            var response = _customCodeClient.SetPageCustomCodes(new[] { new PageCustomCodesListItem { PageId = TestContext.KnownPageIdWithCustomCodes, CustomCode1 = "foo" }, new PageCustomCodesListItem { PageId = TestContext.KnownPageIdWithCustomCodes + 1, CustomCode1 = "bar" } });
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CanSetMultipleCustomCodesWithCsvData()
        {
            var csvString = string.Format("PageId,CustomCode1,CustomCode2,CustomCode3,CustomCode4,CustomCode5,CustomCode6\r\n{0},value1,value2,value3,value4,value5,value6\r\n{1},value1,value2,value3,value4,value5,value6",
                TestContext.KnownPageIdWithCustomCodes, TestContext.KnownPageIdWithCustomCodes + 1);

            var response = _customCodeClient.SetPageCustomCodes(csvString);

            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("a,b")]
        public void CustomCodesAreValidated_Single(string badText)
        {
            var excep = Assert.Throws<ErrorResponseException>(() => _customCodeClient.SetPageCustomCodes(TestContext.KnownPageIdWithCustomCodes, new PageCustomCodes { CustomCode1 = badText }));
            Assert.That(excep.Message.Contains("400"));
        }

        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("a,b")]
        public void CustomCodesAreValidated_Multiple(string badText)
        {
            var response = _customCodeClient.SetPageCustomCodes(new[] { new PageCustomCodesListItem { PageId = TestContext.KnownPageIdWithCustomCodes, CustomCode1 = badText }, new PageCustomCodesListItem { PageId = TestContext.KnownPageIdWithCustomCodes + 1, CustomCode1 = "bar" } });
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Csv()
        {
            var csvString = string.Format("PageId,CustomCode1,CustomCode2,CustomCode3,CustomCode4,CustomCode5,CustomCode6\r\n{0},value1,value2,value3,value44444444444444444444444444444444444444,value5,value6\r\n{1},value1,value2,value3,value4,value5,value6",
                    TestContext.KnownPageIdWithCustomCodes, TestContext.KnownPageIdWithCustomCodes + 1);

            var response = _customCodeClient.SetPageCustomCodes(csvString);

            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        private static DataClientConfiguration XmlDataConfiguration()
        {
            return GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);
        }
    }
}
