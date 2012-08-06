using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "Address", Namespace = "")]
    public class Address
    {
        [DataMember(Order = 10)]
        public string AddressLine1 { get; set; }

        [DataMember(Order = 20)]
        public string AddressLine2 { get; set; }

        [DataMember(Order = 30)]
        public string Town { get; set; }

        [DataMember(Order = 40)]
        public string Region { get; set; }

        [DataMember(Order = 50)]
        public string Postcode { get; set; }

        [DataMember(Order = 60)]
        public string Country { get; set; }
    }
}