using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract(Namespace = "")]
    public class DonationsAndCharges
    {
        [DataMember]
        public double TotalDonations { get; set; }

        [DataMember]
        public double TotalDonationsPlusGiftAid { get; set; }

        [DataMember]
        public double OfflineAmount { get; set; }

        [DataMember]
        public double TargetAmount { get; set; }

        [DataMember]
        public int NumberSmsDonations { get; set; }

        [DataMember]
        public double TotalSmsDonations { get; set; }

        [DataMember]
        public int NumberOnlineDonations { get; set; }

        [DataMember]
        public double TotalDonationsAndGiftAidMinusEstimatedFees { get; set; }

    }
}