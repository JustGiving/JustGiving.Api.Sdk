using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.Summaries
{
    [DataContract(Name = "PageSummary", Namespace = "")]
    public class PageSummary
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "story")]
        public string Story { get; set; }
        [DataMember(Name = "amountRaised")]
        public decimal AmountRaised { get; set; }
        [DataMember(Name = "image")]
        public string Image { get; set; }
        [DataMember(Name = "charityId")]
        public int CharityId { get; set; }
        [DataMember(Name = "eventId")]
        public int EventId { get; set; }
        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract(Name = "CharitySummary", Namespace = "")]
    public class CharitySummary
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }

    [DataContract(Name = "EventSummary", Namespace = "")]
    public class EventSummary
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "date")]
        public DateTime? Date { get; set; }
        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }
}
