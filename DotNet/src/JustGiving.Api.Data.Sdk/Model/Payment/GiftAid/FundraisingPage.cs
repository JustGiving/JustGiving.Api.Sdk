using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "FundraisingPage", Namespace = "")]
    public class FundraisingPage
    {
        public FundraisingPage()
        {
            Fundraiser = new Fundraiser();
            Event = new Event();
            CustomCodes = new CustomCodes();
        }

        [DataMember(Order = 10)]
        public int Id { get; set; }

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

        [DataMember(Order = 100)]
        public Fundraiser Fundraiser { get; set; }

        [DataMember(Order = 110)]
        public Event Event { get; set; }

        [DataMember(Order = 120)]
        public CustomCodes CustomCodes { get; set; }
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

    [DataContract(Name = "Event", Namespace = "")]
    public class Event
    {
        public Event()
        {
            CustomCodes = new EventCustomCodes();
        }

        [DataMember(Order = 10)]
        public string Id { get; set; }

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
}