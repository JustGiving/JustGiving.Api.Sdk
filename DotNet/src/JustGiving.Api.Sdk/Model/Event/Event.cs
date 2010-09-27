using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Event
{
    [DataContract(Name = "event", Namespace = "")]
    public class Event
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "completionDate")]
        public DateTime? CompletionDate { get; set; }
        
        [DataMember(Name = "expiryDate")]
        public DateTime? ExpiryDate { get; set; }
        
        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }
        
        [DataMember(Name = "livePageCount")]
        public int LivePageCount { get; set; }
    }
}
