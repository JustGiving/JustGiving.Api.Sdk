using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Model.Remember;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Namespace = "", Name = "pageRegistration")]
    public class RegisterPageRequest
    {
        /// <summary>
        /// Your reference. (Optional)
        /// </summary>
        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        /// <summary>
        /// The charityId argument specifies the charity to create the fundraising page for. (Required)
        /// </summary>
        [DataMember(Name = "charityId")]
        public int? CharityId { get; set; }

        /// <summary>
        /// The eventId argument specifies the event to create the fundraising page for. This argument must be omitted if an activityType is specified.
        /// </summary>
        [DataMember(Name = "eventId")]
        public int? EventId { get; set; }

        [Obsolete("Please do not use")]
        [DataMember(Name = "eventRef")]
        public string EventRef { get; set; }

        /// <summary>
        /// The pageShortName argument specifies the url you want to assign the new fundraising page. If the short url is available your page will be available at http://www.justgiving.com/{pageShortName} once it is created. (Required)
        /// </summary>
        [DataMember(Name = "pageShortName")]
        public string PageShortName { get; set; }

        /// <summary>
        /// The pageTitle argument allows you to provide a title for the page.
        /// </summary>
        [DataMember(Name = "pageTitle")]
        public string PageTitle { get; set; }
        /// <summary>
        /// The activityType argument specifies the type of activity the page is for. This argument is must be omitted if an eventId is specified.
        /// Birthday - 6
        /// Wedding - 7
        /// Other Celebration - 8
        /// In Memory - 10
        /// </summary>
        [DataMember(Name = "activityType")]
        public ActivityType? ActivityType { get; set; }

        /// <summary>
        /// The targetAmount argument allows you to specify a target amount to raise. (Optional)
        /// </summary>
        [DataMember(Name = "targetAmount")]
        public decimal? TargetAmount { get; set; }

        /// <summary>
        /// The charityOptIn argument allows you to indicate whether the user wishes to opt in to receive communications from the charity the fundraising page is for. (Required)
        /// </summary>
        [DataMember(Name = "charityOptIn")]
        public bool? CharityOptIn { get; set; }

        /// <summary>
        /// The eventDate argument allows you to specify when the event will take place. Required for event activity types (i.e: Birthday, Wedding, OtherCelebration, InMemory).
        /// </summary>
        [DataMember(Name = "eventDate")]
        public DateTime? EventDate { get; set; }

        /// <summary>
        /// The eventName argument allows you to specify a name for the event. Required for event activity types (i.e: Birthday, Wedding, OtherCelebration, InMemory).
        /// </summary>
        [DataMember(Name = "eventName")]
        public string EventName { get; set; }

        /// <summary>
        /// The attribution argument allows you to specify who the fundraising page is attributed to. Required for occasion, organised event and in-memory activity types (i.e: all except Birthday, Wedding).
        /// </summary>
        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }

        /// <summary>
        /// The charityFunded argument specifies whether the charity is contributing in some way to the fundraising, which can affect Gift Aid. For more information about how Gift Aid works http://bit.ly/cZfY1R (Required)
        /// </summary>
        [DataMember(Name = "charityFunded")]
        public bool? IsCharityFunded { get; set; }

        /// <summary>
        /// Deprecated (use CampaignGuid instead)
        /// </summary>

        [Obsolete("Please don't use this any more, use campaign guid instead")]
        [DataMember(Name = "causeId")]
        public int? CauseId { get; set; }

        /// <summary>
        ///  The companyAppealId argument specifies the company appeal you are creating a fundraising page for. (Optional)
        /// </summary>
        [DataMember(Name = "companyAppealId")]
        public int? CompanyAppealId { get; set; }

        /// <summary>
        /// The date the page should expire. This is ignored if you are creating a fundraising page for a pre-defined event.
        /// </summary>
        [DataMember(Name = "expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [DataMember(Name = "pageStory")]
        public string PageStory { get; set; }

        /// <summary>
        /// The 'I'm doing action X' part of the fundraising page summary. (Optional). 50 characters max.
        /// </summary>
        [DataMember(Name = "pageSummaryWhat")]
        public string PageSummaryWhat { get; set; }

        /// <summary>
        /// The 'reason Z' part of the fundraising page summary. (Optional). 50 characters max.
        /// </summary>
        [DataMember(Name = "pageSummaryWhy")]
        public string PageSummaryWhy { get; set; }

        [DataMember(Name = "customCodes")]
        public PageCustomCodes CustomCodes { get; set; }

        [DataMember(Name = "theme")]
        public PageTheme Theme { get; set; }

        /// <summary>
        /// Images must be available over HTTPS and HTTP for optimal user experience
        /// </summary>
        [DataMember(Name = "images", EmitDefaultValue = false)]
        public IList<ImageInfo> Images { get; set; }

        /// <summary>
        /// Videos must be available over HTTPS and HTTP for optimal user experience
        /// </summary>
        [DataMember(Name = "videos", EmitDefaultValue = false)]
        public IList<VideoInfo> Videos { get; set; }

        /// <summary>
        /// In Beta - Details of the Person this page is in memory of, and the page creators relationship to them.
        /// </summary>
        [DataMember(Name = "rememberedPersonReference")]
        public RememberedPersonReference RememberedPersonReference { get; set; }

        /// <summary>
        /// The teamId argument specifies the team to which the fundraising page will be associated with. 
        /// </summary>
        [DataMember(Name = "teamId")]
        public int? TeamId { get; set; }

        /// <summary>
        /// Must be a valid ISO currency code. Will default to GBP if omitted. For a list of valid codes, use the Currency API.
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Provide the campaignGuid of the campaign to link to. (Optional)
        /// </summary>
        [DataMember(Name = "campaignGuid")]
        public Guid? CampaignGuid { get; set; }

        /// <summary>
        /// The isGiftAidable argument specifies whether the page is eligible for Gift Aid (UK Only)
        /// </summary>
        [DataMember(Name = "isGiftAidable")]
        public bool? IsGiftAidable { get; set; }
    }

    [DataContract(Name = "image", Namespace = "")]
    public class ImageInfo
    {
        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "url", IsRequired = true)]
        public string Url { get; set; }

        [DataMember(Name = "isDefault")]
        public bool IsDefault { get; set; }
    }

    [DataContract(Name = "video", Namespace = "")]
    public class VideoInfo
    {
        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "url", IsRequired = true)]
        public string Url { get; set; }

        [DataMember(Name = "isDefault")]
        public bool IsDefault { get; set; }
    }
}