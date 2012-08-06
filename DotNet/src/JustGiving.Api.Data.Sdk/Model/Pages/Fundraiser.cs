using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract]
    public class Fundraiser
    {
        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public bool ConnectedBenefit { get; set; }

        [DataMember]
        public bool FurtherContact { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}