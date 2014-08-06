using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    public class OneSearchTests
    {
        private const string CorrectQuery = "cancer";
        private const int CorrectLimit = 3;
        private const string CorrectCountry = "GB";
        private const string CorrectCategory = "charity";

        [TestCase(WireDataFormat.Json)]
        public void OneSearchIndex_KeywordWithKnownResult_SearchResultsPresent(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var oneSearchClient = new OneSearchApi(client.HttpChannel);
            if (format == WireDataFormat.Json)
            {
                client.HttpClient.AddHeader("Accept", "application/json");
            }

            //act
            var result = oneSearchClient.OneSearchIndex(CorrectQuery, false, "", CorrectLimit, 0, CorrectCountry);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(CorrectQuery, result.Query);
            Assert.AreEqual(CorrectLimit, result.Limit);
            Assert.AreEqual(CorrectCountry, result.Country);
            CollectionAssert.IsNotEmpty(result.GroupedResults);
        }

        [TestCase(WireDataFormat.Json)]
        public void OneSearchIndex_KeywordWithCharityIndex_SearchResultsPresent(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientNoCredentials(format);
            var oneSearchClient = new OneSearchApi(client.HttpChannel);
            if (format == WireDataFormat.Json)
            {
                client.HttpClient.AddHeader("Accept", "application/json");
            }

            //act
            var result = oneSearchClient.OneSearchIndex(CorrectQuery, false, CorrectCategory, CorrectLimit, 0, CorrectCountry);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(CorrectQuery, result.Query);
            Assert.AreEqual(CorrectLimit, result.Limit);
            Assert.AreEqual(CorrectCountry, result.Country);
            Assert.AreEqual(CorrectCategory, result.SpecificIndex);
            CollectionAssert.IsNotEmpty(result.GroupedResults);
        }
    }
}
