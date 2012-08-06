using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract(Namespace = "")]
    public class PageCreated
    {
        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ExpiryDate { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Appeal { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public InMemoriamInfo InMemoriam { get; set; }

        [DataMember]
        public Fundraiser Fundraiser { get; set; }

        [DataMember]
        public Event Event { get; set; }

        [DataMember]
        public ReferralWebsite ReferralWebsite { get; set; }

        [DataMember]
        public DonationsAndCharges DonationsAndCharges { get; set; }

        [DataMember]
        public string CustomCode1 { get; set; }

        [DataMember]
        public string CustomCode2 { get; set; }

        [DataMember]
        public string CustomCode3 { get; set; }

        [DataMember]
        public string CustomCode4 { get; set; }

        [DataMember]
        public string CustomCode5 { get; set; }

        [DataMember]
        public string CustomCode6 { get; set; }

        [DataMember]
        public TeamInfo Team { get; set; }

        [DataMember]
        public OrganisationPortalInfo OrganisationPortal { get; set; }

        [DataMember]
        public string BirthdayName { get; set; }

        [DataMember]
        public string WeddingNames { get; set; }

        [DataMember]
        public bool IsApiCreated { get; set; }
    }
}