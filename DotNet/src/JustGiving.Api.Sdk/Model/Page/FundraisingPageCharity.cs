using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "charity", Namespace = "")]
    public class FundraisingPageCharity
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
    }
}