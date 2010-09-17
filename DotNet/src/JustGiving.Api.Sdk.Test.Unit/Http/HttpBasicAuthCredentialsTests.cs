using System;
using JustGiving.Api.Sdk.Http;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.Http
{
    [TestFixture]
    public class HttpBasicAuthCredentialsTests
    {
        [Test]
        public void Ctor_UserIsEmpty_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new HttpBasicAuthCredentials(string.Empty, "pass"));

            Assert.That(ex.ParamName, Is.StringContaining("user"));
            Assert.That(ex.Message, Is.StringContaining("User is Required for Http Basic Auth"));
        }

        [Test]
        public void Ctor_PassIsEmpty_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new HttpBasicAuthCredentials("user", string.Empty));

            Assert.That(ex.ParamName, Is.StringContaining("pass"));
            Assert.That(ex.Message, Is.StringContaining("Password is Required for Http Basic Auth"));
        }

        [Test]
        public void ToString_UserAndPassSupplied_ReturnsUserPassBase64Encoded()
        {
            const string EXPECTED = "dXNlcjpwYXNz";
            var credentials = new HttpBasicAuthCredentials("user", "pass");
            Assert.That(credentials.ToString(), Is.StringMatching(EXPECTED));
        }
    }
}