using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using GG.Api.Sdk;
using GG.Api.Services.Data.Dto.CustomCodes;
using GG.Api.Services.Data.Sdk.ApiClients;
using NUnit.Framework;

namespace GG.Api.Services.Data.Sdk.Test.Integration
{
    [TestFixture, Category("Slow")]
    public class UpdateEventCustomCodesTests
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
            var response = ccClient.SetEventCustomCodes(TestContext.KnownEventId,
                                                       new EventCustomCodes { CustomCode1 = "foo" });

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
            ccClient.SetEventCustomCodes(TestContext.KnownEventId,
                                                       new EventCustomCodes { CustomCode1 = val });

            var response = ccClient.GetEventCustomCodes(TestContext.KnownEventId);

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
            var response = ccClient.SetEventCustomCodes(new[] { new EventCustomCodesListItem { EventId = TestContext.KnownEventId, CustomCode1 = "foo" }, new EventCustomCodesListItem { EventId = TestContext.KnownEventId + 1, CustomCode1 = "bar" } });

            // assert
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CanSetMultipleCustomCodesWithCsvData()
        {
            // Arrange

            var csvString =
                string.Format(
                    "EventId,CustomCode1,CustomCode2,CustomCode3\r\n{0},value1,value2,value3\r\n{1},value1,value2,value3",
                    TestContext.KnownEventId, TestContext.KnownEventId + 1);


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

            var response = ccClient.SetEventCustomCodes(csvString);

            // assert
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Single()
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
            var response = ccClient.SetEventCustomCodes(TestContext.KnownEventId,
                                                       new EventCustomCodes { CustomCode1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" });

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void CustomCodesAreValidated_Multiple()
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
            var response = ccClient.SetEventCustomCodes(new[] { new EventCustomCodesListItem { EventId = TestContext.KnownEventId, CustomCode1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" }, 
                new EventCustomCodesListItem { EventId = TestContext.KnownEventId + 1, CustomCode1 = "bar" } });

            // assert
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Csv()
        {
            // Arrange

            var csvString =
                string.Format(
                    "EventId,CustomCode1,CustomCode2,CustomCode3\r\n{0},value1,value2,value3\r\n{1},value1,value222222222222222222222222222222222222222,value3",
                    TestContext.KnownEventId, TestContext.KnownEventId + 1);


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

            var response = ccClient.SetEventCustomCodes(csvString);

            // assert
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }
    }
}
