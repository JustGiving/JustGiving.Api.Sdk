using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "Payment", Namespace = "")]
    public class Payment : DtoBase
    {
        public Payment()
        {
            Donations = new List<Donation>();
        }

        [DataMember(Name = "PaymentRef", Order = 0)]
        public int PaymentRef
        {
            get;
            set;
        }

        [DataMember(Name = "Donations", Order = 1)]
        public List<Donation> Donations
        {
            get;
            set;
        }
    }

    [DataContract(Name = "Donor", Namespace = "")]
    public class Donor
    {
        public Donor()
        {
            Address = new Address();
        }

        [DataMember(Order = 10)]
        public int? Id { get; set; }

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

    [DataContract(Name = "EventCustomCodes", Namespace = "")]
    public class EventCustomCodes
    {
        [DataMember(Order = 10)]
        public string CustomCode1 { get; set; }

        [DataMember(Order = 20)]
        public string CustomCode2 { get; set; }

        [DataMember(Order = 30)]
        public string CustomCode3 { get; set; }
    }

    [DataContract(Name = "Event", Namespace = "")]
    public class Event
    {
        public Event()
        {
            CustomCodes = new EventCustomCodes();
        }

        [DataMember(Order = 10)]
        public int? Id { get; set; }

        [DataMember(Order = 20)]
        public string Name { get; set; }

        [DataMember(Order = 30)]
        public DateTime StartDate { get; set; }

        [DataMember(Order = 40)]
        public DateTime ExpiryDate { get; set; }

        [DataMember(Order = 50)]
        public bool IsPromoted { get; set; }

        [DataMember(Order = 60)]
        public string CategoryName { get; set; }

        [DataMember(Order = 70)]
        public bool IsOverSeas { get; set; }

        [DataMember(Order = 80)]
        public bool IsUserCreated { get; set; }

        [DataMember(Order = 90)]
        public EventCustomCodes CustomCodes { get; set; }
    }

    [DataContract(Name = "FundraisingPage", Namespace = "")]
    public class FundraisingPage
    {
        public FundraisingPage()
        {
            Fundraiser = new Fundraiser();
            Event = new Event();
            CustomCodes = new Donations.CustomCodes();
        }

        [DataMember(Order = 10)]
        public int? Id { get; set; }

        [DataMember(Order = 20)]
        public string Title { get; set; }

        [DataMember(Order = 40)]
        public DateTime CreatedDate { get; set; }

        [DataMember(Order = 50)]
        public DateTime ExpiryDate { get; set; }

        [DataMember(Order = 60)]
        public string Url { get; set; }

        [DataMember(Order = 70)]
        public decimal OfflineAmount { get; set; }

        [DataMember(Order = 80)]
        public decimal TargetAmount { get; set; }

        [DataMember(Order = 90)]
        public string Team { get; set; }

        [DataMember(Order = 92)]
        public string TeamUrl { get; set; }

        [DataMember(Order = 94)]
        public string TeamMembers { get; set; }

        [DataMember(Order = 100)]
        public Fundraiser Fundraiser { get; set; }

        [DataMember(Order = 110)]
        public Event Event { get; set; }

        [DataMember(Order = 120)]
        public Donations.CustomCodes CustomCodes { get; set; }

        [DataMember]
        public virtual bool InMemoriamFund { get; set; }

        [DataMember]
        public virtual string InMemoriamName { get; set; }

        [DataMember]
        public virtual string BirthdayName { get; set; }

        [DataMember]
        public virtual string WeddingName { get; set; }

        [DataMember]
        public OrganisationPortal OrganisationPortal { get; set; }
    }

    [DataContract(Name = "CustomCodes", Namespace = "")]
    public class CustomCodes
    {
        [DataMember(Order = 10)]
        public string CustomCode1 { get; set; }

        [DataMember(Order = 20)]
        public string CustomCode2 { get; set; }

        [DataMember(Order = 30)]
        public string CustomCode3 { get; set; }

        [DataMember(Order = 40)]
        public string CustomCode4 { get; set; }

        [DataMember(Order = 50)]
        public string CustomCode5 { get; set; }

        [DataMember(Order = 60)]
        public string CustomCode6 { get; set; }
    }

    [DataContract]
    public class OrganisationPortal
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }

    [DataContract(Name = "Fundraiser", Namespace = "")]
    public class Fundraiser
    {
        public Fundraiser()
        {
            Address = new Address();
        }

        [DataMember(Order = 10)]
        public int? UserId { get; set; }

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
    }

    [DataContract(Name = "ReferralWebsite", Namespace = "")]
    public class ReferralWebsite
    {
        [DataMember(Order = 10)]
        public string Name { get; set; }

        [DataMember(Order = 20)]
        public string Url { get; set; }
    }

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

    [DataContract(Name = "Charges", Namespace = "")]
    public class Charges
    {
        [DataMember(Order = 10)]
        public string PaymentProcessFeeRate { get; set; }

        [DataMember(Order = 20)]
        public decimal PaymentProcessFeeAmount { get; set; }

        [DataMember(Order = 30)]
        public decimal JustGivingTransactionFeeRate { get; set; }

        [DataMember(Order = 40)]
        public decimal JustGivingTransactionFeeAmount { get; set; }

        [DataMember]
        public string SmsOperator { get; set; }

        [DataMember]
        public double SmsOperatorDonorTransactionFee { get; set; }

        [DataMember]
        public double DonationNetAmountPaid { get; set; }

        [DataMember]
        public double JustGivingTransactionFeePaid { get; set; }

        [DataMember]
        public double ProcessingFeePaid { get; set; }

        [DataMember]
        public string CommissionPayer { get; set; }

        [DataMember]
        public double NetTotalChargesByJustGiving { get; set; }
    }

}