using System.Collections.Generic;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.Model.Event
{
    [DataContract(Name = "pagesForEvent", Namespace = "")]
    public class GetPagesForEventResponse
    {
        [DataMember(Name = "eventId")]
        public int EventId { get; set; }

        [DataMember(Name = "totalFundraisingPages")]
        public int TotalFundraisingPages { get; set; }

        [DataMember(Name = "totalPages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "fundraisingPages")]
        public IList<FundraisingPageSummary> FundraisingPageSummaries { get; set; }

        public GetPagesForEventResponse()
        {
            FundraisingPageSummaries = new List<FundraisingPageSummary>();
        }
    }
}