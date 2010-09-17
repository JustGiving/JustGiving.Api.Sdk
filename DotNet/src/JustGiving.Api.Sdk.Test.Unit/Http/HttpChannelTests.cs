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