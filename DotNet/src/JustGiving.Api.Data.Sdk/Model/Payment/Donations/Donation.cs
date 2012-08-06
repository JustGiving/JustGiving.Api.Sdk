using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "Donation", Namespace = "")]
    public class Donation
    {
        public Donation()
        {
            Charges = new Charges();
            Donor = new Donor();
            FundraisingPage = new FundraisingPage();
            ReferralWebsite = new ReferralWebsite();
        }

        [DataMember(Order = 10)]
        public int Id { get; set; }

        [DataMember(Order = 20)]
        public decimal Amount { get; set; }

        [DataMember(Order = 30)]
        public DateTime Date { get; set; }

        [DataMember(Order = 40)]
        public bool IsGiftAidEligible { get; set; }

        [DataMember(Order = 50)]
        public string AppealName { get; set; }

        [DataMember(Order = 70)]
        public string DonationOrigin { get; set; }

        [DataMember(Order = 80)]
        public string Nickname { get; set; }

        [DataMember(Order = 90)]
        public string MessageFromDonor { get; set; }

        [DataMember(Order = 100)]
        public string Source { get; set; }

        [DataMember(Order = 102)]
        public string ProductSource { get; set; }

        [DataMember(Order = 110)]
        public string PaymentFrequency { get; set; }

        [DataMember(Order = 120)]
        public DateTime RecurringCreationDate { get; set; }

        [DataMember(Order = 130)]
        public string PaymentType { get; set; }

        [DataMember(Order = 140)]
        public Charges Charges { get; set; }

        [DataMember(Order = 150)]
        public decimal NetAmount { get; set; }

        [DataMember(Order = 160)]
        public decimal EstimatedVat { get; set; }

        [DataMember(Order = 170)]
        public decimal NetDonationMinusVat { get; set; }

        [DataMember(Order = 180)]
        public Donor Donor { get; set; }

        [DataMember(Order = 190)]
        public FundraisingPage FundraisingPage { get; set; }

        [DataMember(Order = 200)]
        public ReferralWebsite ReferralWebsite { get; set; }
    }
}