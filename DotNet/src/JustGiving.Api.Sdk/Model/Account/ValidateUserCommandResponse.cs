using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Account
{
    [DataContract(Namespace="", Name="validateUser")]
    public class ValidateUserCommandResponse
    {
        [DataMember(Name="isValid")]
        public bool IsValid { get; set; }

        [DataMember(Name="consumerId")]
        public int ConsumerId { get; set;}
    }
}