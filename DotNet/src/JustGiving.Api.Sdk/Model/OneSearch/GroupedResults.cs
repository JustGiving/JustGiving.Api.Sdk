using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;

namespace JustGiving.Api.Sdk.Model.OneSearch
{
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
}
