using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract(Namespace = "")]
    public class OrganisationPortalInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
}