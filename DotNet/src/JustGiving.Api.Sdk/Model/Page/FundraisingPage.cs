using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [KnownType(typeof(FundraisingPage))]
    [KnownType(typeof(FundraisingPageImage))]
    [KnownType(typeof(FundraisingPageBranding))]
    [KnownType(typeof(FundraisingPageCharity))]
    [KnownType(typeof(FundraisingPageMedia))]
    [DataContract(Name = "fundraisingPage", Namespace = "")]
    public class FundraisingPage
    {
        [DataMember(Name = "pageId", EmitDefaultValue = false)]
        public string PageId { get; set; }
        [DataMember(Name = "activityId", EmitDefaultValue = false)]
        public string ActivityId { get; set; }
        [DataMember(Name = "eventName", EmitDefaultValue = false)]
        public string EventName { get; set; }
        [DataMember(Name = "currencySymbol", EmitDefaultValue = false)]
        public string CurrencySymbol { get; set; }
        [DataMember(Name = "pageShortName")]
        public string PageShortName { get; set; }
        [DataMember(Name = "image", EmitDefaultValue = false)]
        public FundraisingPageImage Image { get; set; }
        [DataMember(Name = "imageCount", EmitDefaultValue = false)]
        public string ImageCount { get; set; }
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
        [DataMember(Name = "owner", EmitDefaultValue = false)]
        public string PageCreatorName { get; set; }
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string PageTitle { get; set; }
        [DataMember(Name = "showEventDate", EmitDefaultValue = false)]
        public string ShowEventDate { get; set; }
        [DataMember(Name = "eventDate", EmitDefaultValue = false)]
        public DateTime ActivityDate { get; set; }
        [DataMember(Name = "showExpiryDate", EmitDefaultValue = false)]
        public string ShowExpiryDate { get; set; }
        [DataMember(Name = "expiryDate", EmitDefaultValue = false)]
        public DateTime PageExpiryDate { get; set; }
        [DataMember(Name = "fundraisingTarget", EmitDefaultValue = false)]
        public decimal? TargetAmount { get; set; }
        [DataMember(Name = "totalRaisedPercentageOfFundraisingTarget", EmitDefaultValue = false)]
        public string RaisedRatioPercent { get; set; }
        [DataMember(Name = "totalRaisedOffline", EmitDefaultValue = false)]
        public string OfflineDonations { get; set; }
        [DataMember(Name = "totalRaisedOnline", EmitDefaultValue = false)]
        public string TotalRaisedOnline { get; set; }
        [DataMember(Name = "grandTotalRaisedExcludingGiftAid", EmitDefaultValue = false)]
        public string TotalRaised { get; set; }
        [DataMember(Name = "totalEstimatedGiftAid", EmitDefaultValue = false)]
        public string GiftAidPlusSupplement { get; set; }
        [DataMember(Name = "branding", EmitDefaultValue = false)]
        public FundraisingPageBranding Branding { get; set; }
        [DataMember(Name = "charity", EmitDefaultValue = false)]
        public FundraisingPageCharity Charity { get; set; }
        [DataMember(Name = "media", EmitDefaultValue = false)]
        public FundraisingPageMedia Media { get; set; }
        [DataMember(Name = "rssUrl", EmitDefaultValue = false)]
        public string RssUrl { get; set; }
        [DataMember(Name = "story", EmitDefaultValue = false)]
        public string Story { get; set; }
        [DataMember(Name = "domain")]
        public string Domain { get; set; }
        [DataMember(Name = "smsCode", EmitDefaultValue = false)]
        public string SmsCode { get; set; }
        [DataMember(Name = "companyAppealId", EmitDefaultValue = false)]
        public int? CompanyAppealId { get; set; }
        [DataMember(Name = "attribution", EmitDefaultValue = false)]
        public string Attribution { get; set; }
    }
}