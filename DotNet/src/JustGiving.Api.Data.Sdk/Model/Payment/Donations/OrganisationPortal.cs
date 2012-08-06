using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract]
    public class OrganisationPortal
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
}