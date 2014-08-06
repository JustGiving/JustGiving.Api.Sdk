using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using JustGiving.Api.Sdk.ApiClients;

namespace JustGiving.Api.Sdk.Model.OneSearch
{
    [KnownType(typeof(GroupedResults))]
    [DataContract(Name = "OneSearchResponse", Namespace = "")]
    public class OneSearchResponse
    {
        [DataMember(Name = "GroupedResults")]
        public GroupedResults[] GroupedResults { get; set; }

        [DataMember(Name = "SpecificIndex", Order = 1)]
        public string SpecificIndex { get; set; }

        [DataMember(Name = "Country", Order = 2)]
        public string Country { get; set; }

        [DataMember(Name = "Query", Order = 3)]
        public string Query { get; set; }

        [DataMember(Name = "Limit", Order = 4)]
        public int Limit { get; set; }

        [DataMember(Name = "Total", Order = 5)]
        public int Total { get; set; }
    }
}
