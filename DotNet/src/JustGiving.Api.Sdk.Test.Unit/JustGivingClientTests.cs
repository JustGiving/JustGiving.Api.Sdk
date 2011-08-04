using System;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.MicrosoftHttp;
using JustGiving.Api.Sdk.Model;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit
{
    [TestFixture]
    [Category("Fast")]
    class JustGivingClientTests
    {
        [Test]
        public void Ctor_WhenHttpClientIsProvidedAndValid_Constructs()
        {
            var client = new HttpClientWrapper();
            new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1), client);
        }

        [Test]
        public void Ctor_WhenHttpClientIsProvidedAndNull_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1), null));

            Assert.That(exception.ParamName, Is.StringContaining("httpClient"));
            Assert.That(exception.Message, Is.StringContaining("httpClient must not be null to access the api."));
        }
    }
}
