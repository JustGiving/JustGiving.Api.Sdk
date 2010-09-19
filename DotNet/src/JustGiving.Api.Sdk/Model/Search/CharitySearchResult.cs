using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Search
{
    [DataContract(Name = "charitySearchResult", Namespace = "")]
    public class CharitySearchResult
    {
        [DataMember(Name = "charityId")]
        public string CharityId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "registrationNumber")]
        public string RegistrationNumber { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "logoFileName")]
        public string LogoFileName { get; set; }
    }
}