using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;

namespace JustGiving.Api.Sdk.Model.OneSearch
{
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
}
