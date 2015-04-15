using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class CurrencyApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Countries_WhenDoesntSuppliedCredentials_ReturnsCountries(WireDataFormat format)
        {
            //arrange 
            var client = TestContext.CreateClientNoCredentials(format);
            var currencyResources = new CurrencyApi(client.HttpChannel);

            //act
            var result = currencyResources.ValidCurrencyCodes();

            //assert
            CollectionAssert.IsNotEmpty(result);
        }
    }
}
