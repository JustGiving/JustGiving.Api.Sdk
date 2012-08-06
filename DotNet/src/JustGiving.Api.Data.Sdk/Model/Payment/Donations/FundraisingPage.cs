using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
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
}