using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Search
{
    [DataContract(Name = "eventSearch", Namespace = "")]
    public class EventSearchResults
    {
        [DataMember(Name = "query")]
        public string Query { get; set; }

        [DataMember(Name = "numberOfHits")]
        public int NumberOfHits { get; set; }

        [DataMember(Name = "totalPages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "prev")]
        public RestResponsePrevElement Prev { get; set; }

        [DataMember(Name = "next")]
        public RestResponseNextElement Next { get; set; }

        [DataMember(Name = "events")]
        public EventSearchResult[] Results { get; set; }
    }
}
