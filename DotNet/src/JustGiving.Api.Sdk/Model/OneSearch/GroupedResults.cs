using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.ApiClients;

namespace JustGiving.Api.Sdk.Model.OneSearch
{
    [KnownType(typeof(Results))]
    [DataContract(Name = "GroupedResults", Namespace = "")]
    public class GroupedResults
    {
        [DataMember(Name = "Title", Order = 0)]
        public string Title { get; set; }

        [DataMember(Name = "Count", Order = 1)]
        public int Count { get; set; }

        [DataMember(Name = "Results", Order = 2)]
        public Results[] Results { get; set; }

        [DataMember(Name = "Specific", Order = 3)]
        public string Specific { get; set; }
    }
}
