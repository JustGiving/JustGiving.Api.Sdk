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

        public FundraisingPageSummary()
        {
            PageImages = new List<string>();
        }
    }
}