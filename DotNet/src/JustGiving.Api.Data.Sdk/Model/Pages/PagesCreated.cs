using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [CollectionDataContract(Namespace = "")]
    public class PagesCreated : IEnumerable<PageCreated>
    {
        public PagesCreated(IEnumerable<PageCreated> innerCollection)
        {
            Pages = innerCollection.ToList();
        }

        public PagesCreated()
            : this(new List<PageCreated>())
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PageCreated> GetEnumerator()
        {
            return Pages.GetEnumerator();
        }

        public void Add(PageCreated pageCreated)
        {
            Pages.Add(pageCreated);
        }

        [DataMember]
        public List<PageCreated> Pages { get; set; }
    }

    [DataContract(Namespace = "")]
    public class InMemoriamInfo
    {
        [DataMember]
        public bool IsInMemoriam { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Town { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public DateTime? DateOfDeath { get; set; }

        [DataMember]
        public string Relationship { get; set; }

        public static implicit operator bool(InMemoriamInfo info)
        {
            return info != null && info.IsInMemoriam;
        }
    }

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

    [DataContract]
    public class ReferralWebsite
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }

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

    [DataContract(Namespace = "")]
    public class OrganisationPortalInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }

    [DataContract(Namespace = "")]
    public class TeamInfo
    {
        [DataMember]
        public string Members { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class Fundraiser
    {
        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public bool ConnectedBenefit { get; set; }

        [DataMember]
        public bool FurtherContact { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public string Email { get; set; }
    }

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
