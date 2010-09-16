using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Account
{
    [DataContract(Namespace = "", Name = "accountRegistration")]
    public class AccountRegistrationConfirmation
    {
        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }
    }
}