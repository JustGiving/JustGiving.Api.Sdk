using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract(Namespace = "")]
    public class Event
    {
        [DataMember]
        public bool IsPromoted { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string ExpiryDate { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public bool IsUserCreated { get; set; }

        [DataMember]
        public bool IsOverseas { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string CustomCode1 { get; set; }

        [DataMember]
        public string CustomCode2 { get; set; }

        [DataMember]
        public string CustomCode3 { get; set; }
    }
}