using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Charity
{
    [DataContract(Name = "authentication", Namespace = "")]
    public class AuthenticateCharityUserRequest
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "pin")]
        public string Pin { get; set; }
    }
}