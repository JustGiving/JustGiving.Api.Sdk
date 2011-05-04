using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Charity
{
    [DataContract(Name = "charity", Namespace = "")]
    public class Charity
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "logoUrl")]
        public string LogoUrl { get; set; }

        [DataMember(Name = "profilePageUrl")]
        public string ProfilePageUrl { get; set; }

        [DataMember(Name = "registrationNumber")]
        public string RegistrationNumber { get; set; }

        [DataMember(Name = "websiteUrl")]
        public string WebsiteUrl { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "pageShortName")]
        public string PageShortName { get; set; }

        [DataMember(Name = "smsCode")]
        public string SmsCode { get; set; }

        [DataMember(Name="mobileAppeals")]
        public IEnumerable<MobileAppeal> MobileAppeals { get; set; }
    }
}
