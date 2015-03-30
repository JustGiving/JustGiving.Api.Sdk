using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class CountryApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Countries_WhenDoesntSuppliedCredentials_ReturnsCountries(WireDataFormat format)
        {
            //arrange 
            var client = TestContext.CreateClientNoCredentials(format);
            var countryResources = new CountryApi(client.HttpChannel);

            //act
            var result = countryResources.Countries();

            //assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
