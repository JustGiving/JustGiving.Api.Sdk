using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class OneSearchApi : ApiClientBase, IOneSearchApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/onesearch"; }
        }
        public OneSearchApi(HttpChannel channel)
            : base(channel)
        {

        }

        public OneSearchResponse OneSearchIndex(string phraseToSearch, bool groupSearch = false,
            string resultsByIndex = "",
            int limit = 0, int offset = 0, string country = "GB")
        {
            string locationFormat = OneSearchQueryFormat(phraseToSearch, groupSearch, resultsByIndex, limit, offset,
                country);
            return HttpChannel.PerformRequest<OneSearchResponse>("GET", locationFormat);
        }

        public OneSearchResponse OneSearchIndex(string phraseToSearch)
        {
            return OneSearchIndex(phraseToSearch, false, "", 3, 0, "GB");
        }

        private string OneSearchQueryFormat(string phraseToSearch, bool groupSearch,
            string resultsByIndex, int limit, int offset, string country)
        {
            var locationFormat = ResourceBase;
            locationFormat += "?q=" + Uri.EscapeDataString(phraseToSearch ?? string.Empty);
            //locationFormat += "&g=" + groupSearch;
            //locationFormat += "&i=" + resultsByIndex;
            //locationFormat += "&limit=" + limit;
            //locationFormat += "&offset=" + offset;
            locationFormat += "&country=" + country;
            return locationFormat;
        }
    }
    public interface IOneSearchApi
    {
        OneSearchResponse OneSearchIndex(string phraseToSearch, bool groupSearch = false, string resultsByIndex = "",
            int limit = 0, int offset = 0, string country = "GB");

        OneSearchResponse OneSearchIndex(string phraseToSearch);
    }

    [DataContract(Name = "OneSearchResponse", Namespace = "")]
    public class OneSearchResponse
    {
        [DataMember(Name = "GroupedResults")]
        public List<GroupedResults> GroupedResults { get; set; }

        [DataMember(Name = "SpecificIndex")]
        public string SpecificIndex { get; set; }

        [DataMember(Name = "Country")]
        public string Country { get; set; }

        [DataMember(Name = "Query")]
        public string Query { get; set; }

        [DataMember(Name = "Limit")]
        public int Limit { get; set; }

        [DataMember(Name = "Total")]
        public int Total { get; set; }

        public OneSearchResponse()
        {
            GroupedResults = new List<GroupedResults>();
        }
    }

    [DataContract(Name = "GroupedResults", Namespace = "")]
    public class GroupedResults
    {
        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "Count")]
        public int Count { get; set; }

        [DataMember(Name = "Results")]
        public List<Results> Results { get; set; }

        [DataMember(Name = "Specific")]
        public string Specific { get; set; }

        public GroupedResults()
        {
            Results = new List<Results>();
        }
    }

    [DataContract(Name = "Results", Namespace = "")]
    public class Results
    {
        [DataMember(Name = "EventIds")]
        public int[] EventIds { get; set; }

        [DataMember(Name = "Subtext")]
        public string Subtext { get; set; }

        [DataMember(Name = "Link")]
        public string Link { get; set; }

        [DataMember(Name = "LinkPath")]
        public string LinkPath { get; set; }

        [DataMember(Name = "CountryCode")]
        public string CountryCode { get; set; }

        [DataMember(Name = "ProfileWhat")]
        public string ProfileWhat { get; set; }

        [DataMember(Name = "ProfileWhy")]
        public string ProfileWhy { get; set; }

        [DataMember(Name = "Highlight")]
        public string Highlight { get; set; }

        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Logo")]
        public string Logo { get; set; }

        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "Score")]
        public double Score { get; set; }
    }
}
