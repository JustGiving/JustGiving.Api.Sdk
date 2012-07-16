using System;
using GG.Api.Sdk.Test.Unit;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Unit
{
    [TestFixture]
    [Category("Fast")]
    public class ClientConfigurationTests
    {
        [TestCase(null)]
        [TestCase("")]
        public void Ctor_WhenDomainRootIsNullOrEmpty_ThrowsArgumentNullException(string root)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ClientConfiguration(root, TestContext.ApiKey, 1));

            Assert.That(exception.ParamName, Is.StringContaining("rootDomain"));
            Assert.That(exception.Message, Is.StringContaining("rootDomain is required."));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Ctor_WhenApiKeyIsNullOrEmpty_ThrowsArgumentNullException(string apiKey)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ClientConfiguration(TestContext.ApiLocation, apiKey, 1));

            Assert.That(exception.ParamName, Is.StringContaining("apiKey"));
            Assert.That(exception.Message, Is.StringContaining("apiKey is required."));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Ctor_WhenApiKeyIsNullOrEmpty_ThrowsArgumentNullException(int apiVersion)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, apiVersion));

            Assert.That(exception.ParamName, Is.StringContaining("apiVersion"));
            Assert.That(exception.Message, Is.StringContaining("apiVersion must be valid."));
        }
    }
}
