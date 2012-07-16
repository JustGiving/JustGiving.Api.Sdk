using System;
using System.Linq;
using System.Net;
using GG.Api.Sdk;
using GG.Api.Services.Data.Dto.CustomCodes;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class UpdatePageCustomCodesTests
    {
        [Test]
        public void CanSetCustomCode()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
                                          {
                                              WireDataFormat = WireDataFormat.Xml,
                                              IsZipSupportedByClient = false,
                                              Username = TestContext.TestUsername,
                                              Password = TestContext.TestValidPassword,
                                              ConnectionTimeOut = TimeSpan.FromMinutes(20)
                                          };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            // Act
            var response = ccClient.SetPageCustomCodes(TestContext.KnownPageId,
                                                       new PageCustomCodes {CustomCode1 = "foo"});

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CanGetCustomCode()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword,
                ConnectionTimeOut = TimeSpan.FromMinutes(20)
            };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            // Act
            var val = Guid.NewGuid().ToString().Substring(0, 5);
            ccClient.SetPageCustomCodes(TestContext.KnownPageId, new PageCustomCodes {CustomCode1 = val});

            var response = ccClient.GetPageCustomCodes(TestContext.KnownPageId);

            // assert
            Assert.That(response.CustomCode1, Is.EqualTo(val));
        }

        [Test]
        public void CanSetMultipleCustomCodes()
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword,
                ConnectionTimeOut = TimeSpan.FromMinutes(20)
            };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            // Act
            var response = ccClient.SetPageCustomCodes(new[] { new PageCustomCodesListItem { PageId = TestContext.KnownPageId, CustomCode1 = "foo" }, new PageCustomCodesListItem { PageId = TestContext.KnownPageId + 1, CustomCode1 = "bar" } });

            // assert
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CanSetMultipleCustomCodesWithCsvData()
        {
            // Arrange

            var csvString =
                string.Format(
                    "PageId,CustomCode1,CustomCode2,CustomCode3,CustomCode4,CustomCode5,CustomCode6\r\n{0},value1,value2,value3,value4,value5,value6\r\n{1},value1,value2,value3,value4,value5,value6",
                    TestContext.KnownPageId, TestContext.KnownPageId + 1);


            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword,
                ConnectionTimeOut = TimeSpan.FromMinutes(20)
            };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            var response = ccClient.SetPageCustomCodes(csvString);

            // assert
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("a,b")]
        public void CustomCodesAreValidated_Single(string badText)
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword,
                ConnectionTimeOut = TimeSpan.FromMinutes(20)
            };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            // Act
            var response = ccClient.SetPageCustomCodes(TestContext.KnownPageId,
                                                       new PageCustomCodes { CustomCode1 = badText });

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [TestCase("a,b")]
        public void CustomCodesAreValidated_Multiple(string badText)
        {
            // Arrange
            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword,
                ConnectionTimeOut = TimeSpan.FromMinutes(20)
            };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            // Act
            var response = ccClient.SetPageCustomCodes(new[] { new PageCustomCodesListItem { PageId = TestContext.KnownPageId, CustomCode1 = badText }, new PageCustomCodesListItem { PageId = TestContext.KnownPageId + 1, CustomCode1 = "bar" } });


            // assert
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Csv()
        {
            // Arrange

            var csvString =
                string.Format(
                    "PageId,CustomCode1,CustomCode2,CustomCode3,CustomCode4,CustomCode5,CustomCode6\r\n{0},value1,value2,value3,value44444444444444444444444444444444444444,value5,value6\r\n{1},value1,value2,value3,value4,value5,value6",
                    TestContext.KnownPageId, TestContext.KnownPageId + 1);


            var clientConfiguration = new ClientConfiguration
            {
                WireDataFormat = WireDataFormat.Xml,
                IsZipSupportedByClient = false,
                Username = TestContext.TestUsername,
                Password = TestContext.TestValidPassword,
                ConnectionTimeOut = TimeSpan.FromMinutes(20)
            };

            var client = new JustGivingClient(clientConfiguration);
            var ccClient = new CustomCodesClient(client);

            var response = ccClient.SetPageCustomCodes(csvString);

            // assert
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }
    }
}
