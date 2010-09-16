using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Donation
{
    [DataContract(Namespace = "", Name = "donationResult")]
    public class DonationStatus
    {
        [DataMember(Name = "ref")]
        public string Reference { get; set; }

        [DataMember(Name = "donationId")]
        public int DonationId { get; set; }

        [DataMember(Name = "donationRef")]
        public string DonationRef { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }
    }
}
