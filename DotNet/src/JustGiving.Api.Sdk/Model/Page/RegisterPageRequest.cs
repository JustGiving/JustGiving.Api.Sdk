using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Model.Remember;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Namespace = "", Name = "pageRegistration")]
    public class RegisterPageRequest
    {
        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        [DataMember(Name = "charityId")]
        public int CharityId { get; set; }

        [DataMember(Name = "eventId")]
        public int? EventId { get; set; }

        [DataMember(Name = "eventRef")]
        public string EventRef { get; set; }

        [DataMember(Name = "pageShortName")]
        public string PageShortName { get; set; }

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

        [DataMember(Name = "targetAmount")]
        public decimal? TargetAmount { get; set; }

        [DataMember(Name = "justGivingOptIn")]
        public bool JustGivingOptIn { get; set; }

        [DataMember(Name = "charityOptIn")]
        public bool CharityOptIn { get; set; }

        [DataMember(Name = "eventDate")]
        public DateTime? EventDate { get; set; }

        [DataMember(Name = "eventName")]
        public string EventName { get; set; }

        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }

        [DataMember(Name = "charityFunded")]
        public bool IsCharityFunded { get; set; }

        [DataMember(Name = "causeId")]
        public int? CauseId { get; set; }

        [DataMember(Name = "companyAppealId")]
        public int? CompanyAppealId { get; set; }

        [DataMember(Name = "expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [DataMember(Name = "pageStory")]
        public string PageStory { get; set; }

        [DataMember(Name = "customCodes")]
        public PageCustomCodes CustomCodes { get; set; }

        [DataMember(Name = "theme")]
        public PageTheme Theme { get; set; }

        [DataMember(Name = "images", EmitDefaultValue = false)]
        public IList<ImageInfo> Images { get; set; }

        [DataMember(Name = "videos", EmitDefaultValue = false)]
        public IList<VideoInfo> Videos { get; set; }

        [DataMember(Name = "rememberedPersonReference")]
        public RememberedPersonReference RememberedPersonReference { get; set; }
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