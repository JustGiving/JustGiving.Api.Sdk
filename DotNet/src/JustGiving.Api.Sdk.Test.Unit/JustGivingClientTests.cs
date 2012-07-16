using System;
using GG.Api.Sdk.Test.Unit;
using JustGiving.Api.Sdk.Http.MicrosoftHttp;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Unit
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
