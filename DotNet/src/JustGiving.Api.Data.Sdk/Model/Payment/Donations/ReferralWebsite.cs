using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "ReferralWebsite", Namespace = "")]
    public class ReferralWebsite
    {
        [DataMember(Order = 10)]
        public string Name { get; set; }

        [DataMember(Order = 20)]
        public string Url { get; set; }
    }
}