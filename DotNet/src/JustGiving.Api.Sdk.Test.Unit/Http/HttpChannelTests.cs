using System;
using System.Net;
using System.Text;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.Http
{
    [TestFixture]
    [Category("Fast")]
    public class HttpChannelTests
    {
        private static MockHttpClient<object> PerformGenericApiRequest(Action<HttpChannel> action)
        {
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            var channel = new HttpChannel(TestContext.Configuration, client);
            action(channel);
            return client;
        }

        private static MockHttpClient<object> PerformGenericApiRequest(Action<ClientConfiguration, HttpChannel> action)
        {
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            var config = TestContext.Configuration;
            var channel = new HttpChannel(config, client);
            action(config, channel);
            return client;
        }

        [Test]
        public void PerformApiRequest_LocationFormatDoesNotContainApiKeyPlaceholder_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => PerformGenericApiRequest(c => c.PerformApiRequest<object>("GET", "http://test.com/{apiVersion}")));

            Assert.That(ex.ParamName, Is.StringMatching("locationFormat"));
            Assert.That(ex.Message, Is.StringContaining("'locationFormat must contain '{apiKey}' and '{apiVersion}' placeholders (case sensitive)."));
        }

        [Test]
        public void PerformApiRequest_LocationFormatDoesNotContainApiVersionPlaceholder_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => PerformGenericApiRequest(c => c.PerformApiRequest<object>("GET", "http://test.com/{apiKey}")));

            Assert.That(ex.ParamName, Is.StringMatching("locationFormat"));
            Assert.That(ex.Message, Is.StringContaining("'locationFormat must contain '{apiKey}' and '{apiVersion}' placeholders (case sensitive)."));
        }

        [Test]
        public void PerformApiRequest_LocationFormatContainsApiKeyAndApiVersionPlaceholder_CallsCorrectUrl()
        {
            var client = PerformGenericApiRequest(
                (config, channel) =>
                    {
                        config.ApiKey = "test-key";
                        config.ApiVersion = 42;
                        channel.PerformApiRequest<object>("GET", "http://test.com/{apiKey}/vers-{apiVersion}");
                    });

            Assert.That(client.LastRequestedUrl, Is.StringMatching(
                "http://test.com/test-key/vers-42"));
        }

        [Test]
        public void PerformApiRequest_MethodPOST_SendsRequest()
        {
            var client = PerformGenericApiRequest(
                c => c.PerformApiRequest<object>("POST", "http://test.com/{apiKey}/vers-{apiVersion}"));

            Assert.NotNull(client.LastRequest);
        }

        [Test]
        public void PerformApiRequest_MethodGET_SendsRequest()
        {
            var client = PerformGenericApiRequest(
                c => c.PerformApiRequest<object>("GET", "http://test.com/{apiKey}/vers-{apiVersion}"));

            Assert.NotNull(client.LastRequest);
        }

        [Test]
        public void PerformApiRequest_MethodHEAD_SendsRequest()
        {
            var client = PerformGenericApiRequest(
                c => c.PerformApiRequest<object>("HEAD", "http://test.com/{apiKey}/vers-{apiVersion}"));

            Assert.NotNull(client.LastRequest);
        }

        [Test]
        public void PerformApiRequest_MethodPUT_SendsRequest()
        {
            var client = PerformGenericApiRequest(
                c => c.PerformApiRequest<object>("PUT", "http://test.com/{apiKey}/vers-{apiVersion}"));

            Assert.NotNull(client.LastRequest);
        }

        [Test]
        public void PerformApiRequest_MethodInvalid_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => PerformGenericApiRequest(
                    c => c.PerformApiRequest<object>("NotUsedVerb", "http://test.com/{apiKey}/vers-{apiVersion}")));

            Assert.That(ex.ParamName, Is.StringMatching("method"));
            Assert.That(ex.Message, Is.StringContaining("Invalid Http Method - Currently Supported Methods are GET, POST, PUT and HEAD"));
        }

        [Test]
        public void Ctor_UserNameAndPasswordSpecified_SetsBasicAuthenticationHeader()
        {
            var config = new ClientConfiguration("test") { Username = "user", Password = "pass" };
            var client = new MockHttpClient<object>(HttpStatusCode.OK);
            new HttpChannel(config, client);

            var header = client.Headers["Authorization"];

            var credentials = new HttpBasicAuthCredentials("user", "pass").ToString();

            Assert.That(header, Is.StringContaining("Basic " + credentials));
        }
    }
}