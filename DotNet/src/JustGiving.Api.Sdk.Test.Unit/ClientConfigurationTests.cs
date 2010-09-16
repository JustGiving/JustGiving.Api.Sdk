using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit
{
    [TestFixture]
    public class ClientConfigurationTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Ctor_WhenDomainRootIsNullEmptyOrWhitespace_ThrowsArgumentNullException(string root)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ClientConfiguration(root, "000", 1));

            Assert.That(exception.ParamName, Is.StringContaining("rootDomain"));
            Assert.That(exception.Message, Is.StringContaining("rootDomain is required."));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Ctor_WhenApiKeyIsNullEmptyOrWhitespace_ThrowsArgumentNullException(string apiKey)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ClientConfiguration("http://api.local.justgiving.com/", apiKey, 1));

            Assert.That(exception.ParamName, Is.StringContaining("apiKey"));
            Assert.That(exception.Message, Is.StringContaining("apiKey is required."));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Ctor_WhenApiKeyIsNullEmptyOrWhitespace_ThrowsArgumentNullException(int apiVersion)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new ClientConfiguration("http://api.local.justgiving.com/", "000", apiVersion));

            Assert.That(exception.ParamName, Is.StringContaining("apiVersion"));
            Assert.That(exception.Message, Is.StringContaining("apiVersion must be valid."));
        }
    }
}
