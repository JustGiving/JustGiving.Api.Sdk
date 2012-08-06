using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public string Line1 { get; set; }

        [DataMember]
        public string Line2 { get; set; }

        [DataMember]
        public string Town { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string Postcode { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}