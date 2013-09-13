using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "fundraisingPages", Namespace = "")]
    public class FundraisingPageSummariesPaginated
    {
        [DataMember(Name = "results")]
        public List<FundraisingPageSummary> FundraisingPageSummaries { get; set; }

        [DataMember(Name = "pagination")]
        public Pagination Pagination { get; set; }
    }
}