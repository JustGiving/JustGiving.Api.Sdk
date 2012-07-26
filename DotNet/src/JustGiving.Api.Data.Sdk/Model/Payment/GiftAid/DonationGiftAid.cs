using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "Donation", Namespace = "")]
    public class DonationGiftAid
    {
        public DonationGiftAid()
        {
            Charges = new Charges();
            Donor = new Donor();
            FundraisingPage = new FundraisingPage();
            ReferralWebsite = new ReferralWebsite();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string AppealName { get; set; }

        [DataMember]
        public string DonationOrigin { get; set; }

        [DataMember]
        public string Nickname { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string PaymentFrequency { get; set; }

        [DataMember]
        public string RecurringCreationDate { get; set; }

        [DataMember]
        public string PaymentType { get; set; }

        [DataMember]
        public Charges Charges { get; set; }

        [DataMember]
        public decimal GrossGiftAidAndTransitionalRelief { get; set; }

        [DataMember]
        public decimal GrossGiftAidPayable { get; set; }

        [DataMember]
        public decimal GrossTransitionalReliefPayable { get; set; }

        [DataMember]
        public decimal NetGiftAidAmount { get; set; }

        [DataMember]
        public decimal EstimatedVat { get; set; }

        [DataMember]
        public decimal NetGiftAidMinusEstimatedVat { get; set; }

        [DataMember]
        public Donor Donor { get; set; }

        [DataMember]
        public FundraisingPage FundraisingPage { get; set; }

        [DataMember]
        public ReferralWebsite ReferralWebsite { get; set; }

        [DataMember]
        public virtual string SmsOperator { get; set; }

        [DataMember]
        public virtual double SmsOperatorDonorTransactionFee { get; set; }
    }

    [DataContract(Name = "ReferralWebsite", Namespace = "")]
    public class ReferralWebsite
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }

    [DataContract(Name = "Charges", Namespace = "")]
    public class Charges
    {
        [DataMember(Order = 10)]
        public string CardProcessFeeRate { get; set; }

        [DataMember(Order = 20)]
        public decimal CardProcessFeeAmount { get; set; }

        [DataMember(Order = 30)]
        public decimal JustGivingTransactionFeeRate { get; set; }

        [DataMember(Order = 40)]
        public decimal JustGivingTransactionFeeAmount { get; set; }
    }

    [DataContract(Name = "Donor", Namespace = "")]
    public class Donor
    {
        public Donor()
        {
            Address = new Address();
        }

        [DataMember(Order = 10)]
        public int Id { get; set; }

        [DataMember(Order = 20)]
        public string Title { get; set; }

        [DataMember(Order = 30)]
        public string FirstName { get; set; }

        [DataMember(Order = 40)]
        public string LastName { get; set; }

        [DataMember(Order = 50)]
        public string Email { get; set; }

        [DataMember(Order = 60)]
        public Address Address { get; set; }

        [DataMember(Order = 70)]
        public bool IsFurtherContact { get; set; }

        [DataMember(Order = 80)]
        public bool IsUKTaxPayer { get; set; }

        [DataMember(Order = 90)]
        public bool IsConnected { get; set; }

    }
}