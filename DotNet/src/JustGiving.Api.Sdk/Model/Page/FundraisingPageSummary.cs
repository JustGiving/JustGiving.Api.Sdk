using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Namespace = "", Name = "fundraisingPage")]
    public class FundraisingPageSummary
    {
        [DataMember(Name = "pageId")]
        public int PageId { get; set; }
        [DataMember(Name = "pageTitle")]
        public string PageTitle { get; set; }
        [DataMember(Name = "pageStatus")]
        public string PageStatus { get; set; }
        [DataMember(Name = "pageShortName")]
        public string PageShortName { get; set; }
        [DataMember(Name = "raisedAmount")]
        public decimal RaisedAmount { get; set; }
        [DataMember(Name = "designId")]
        public int DesignId { get; set; }
        [DataMember(Name = "targetAmount")]
        public decimal TargetAmount { get; set; }
        [DataMember(Name = "offlineDonations")]
        public decimal OfflineDonations { get; set; }
        [DataMember(Name = "totalRaisedOnline")]
        public decimal TotalRaisedOnline { get; set; }
        [DataMember(Name = "giftAidPlusSupplement")]
        public decimal GiftAidPlusSupplement { get; set; }
        [DataMember(Name = "pageImages")]
        public List<string> PageImages { get; set; }
        [DataMember(Name = "eventName")]
        public string EventName { get; set; }
        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        [DataMember(Name = "inMemoryPerson")]
        public InMemoryPerson InMemoryPerson { get; set; }
        [DataMember(Name = "charityId", Order = 13)]
        public string CharityId { get; set; }
        [DataMember(Name = "smsCode", Order = 14)]
        public string SmsCode { get; set; }

        public FundraisingPageSummary()
        {
            PageImages = new List<string>();
        }
    }

    [DataContract(Namespace = "", Name = "inMemoryPerson")]
    public class InMemoryPerson
    {
        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "LastName")]
        public string LastName { get; set; }
        [DataMember(Name = "dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [DataMember(Name = "dateOfDeath")]
        public DateTime DateOfDeath { get; set; }
    }
}