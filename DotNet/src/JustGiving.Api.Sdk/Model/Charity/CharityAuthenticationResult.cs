using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Charity
{
    [DataContract(Name = "authenticationResponse", Namespace="")]
    public class CharityAuthenticationResult
    {
        [DataMember(Name = "isValid")]
        public bool IsValid { get; set; }

        [DataMember(Name = "charityId")]
        public int CharityId { get; set; }
    }
}