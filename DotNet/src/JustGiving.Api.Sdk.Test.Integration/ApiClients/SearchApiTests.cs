using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class SearchApiTests
    {
        [Test]
        public void CharitySearch_KeywordWithKnownResults_SearchResultsPresent()
        {
            var client = new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1) { WireDataFormat = WireDataFormat.Json, });
            var searchClient = new SearchApi(client);
            
            var items = searchClient.CharitySearch("cancer");
        }
    }
}
