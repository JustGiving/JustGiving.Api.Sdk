using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Search
{
    [DataContract(Name = "eventSummary", Namespace = "")]
    public class EventSearchResult
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "numberOfLivePages")]
        public int NumberOfLivePages { get; set; }

        [DataMember(Name = "isManaged")]
        public bool IsManaged { get; set; }

        [DataMember(Name = "expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [DataMember(Name = "completionDate")]
        public DateTime? CompletionDate { get; set; }

        [DataMember(Name = "categoryId")]
        public int CategoryId { get; set; }

        [DataMember(Name = "amountRaised")]
        public decimal AmountRaised { get; set; }

        [DataMember(Name = "amountGiftAid")]
        public decimal AmountGiftAid { get; set; }
    }
}