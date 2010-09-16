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
            var client = new JustGivingClient(new ClientConfiguration("http://api.local.justgiving.com/", "000", 1));
            var searchClient = new SearchApi(client);

            var items = searchClient.CharitySearch("demo");
        }
    }
}
