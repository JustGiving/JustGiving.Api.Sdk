using System;
using System.Net;
using System.Text;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.Http
{
    [TestFixture]
    public class HttpChannelTests
    {
        [Test]
        public void PerformApiRequest_LocationFormatDoesNotContainApiKeyPlaceholder_ThrowsArgumentException()
        {
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass" };
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            var channel = new HttpChannel(config, client);

            var ex = Assert.Throws<ArgumentException>(
                () => channel.PerformApiRequest<object>("GET", "http://test.com/{apiVersion}"));
            Assert.That(ex.ParamName, Is.StringMatching("locationFormat"));
            Assert.That(ex.Message, Is.StringContaining("'locationFormat must contain '{apiKey}' and '{apiVersion}' placeholders (case sensitive)."));
        }

        [Test]
        public void PerformApiRequest_LocationFormatDoesNotContainApiVersionPlaceholder_ThrowsArgumentException()
        {
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass" };
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            var channel = new HttpChannel(config, client);

            var ex = Assert.Throws<ArgumentException>(
                () => channel.PerformApiRequest<object>("GET", "http://test.com/{apiKey}"));
            Assert.That(ex.ParamName, Is.StringMatching("locationFormat"));
            Assert.That(ex.Message, Is.StringContaining("'locationFormat must contain '{apiKey}' and '{apiVersion}' placeholders (case sensitive)."));
        }

        [Test]
        public void PerformApiRequest_LocationFormatContainsApiKeyAndApiVersionPlaceholder_CallsCorrectUrl()
        {
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass", ApiKey = "test-key", ApiVersion = 42};
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            var channel = new HttpChannel(config, client);

            channel.PerformApiRequest<object>("GET", "http://test.com/{apiKey}/vers-{apiVersion}");

            Assert.That(client.LastRequestedUrl, Is.StringMatching(
                "http://test.com/test-key/vers-42"));
        }

        [Test]
        public void Ctor_UserNameAndPasswordSpecified_SetsBasicAuthenticationHeader()
        {
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass" };
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            new HttpChannel(config, client);

            var header = client.Headers["Authorization"];

            // TODO: Move the Credentials to an Object (Encapsulate the Base64 Encoding).
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("user:pass"));

            Assert.That(header, Is.StringContaining("Basic " + credentials));
        }
    }
}