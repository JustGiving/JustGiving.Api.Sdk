using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "ReferralWebsite", Namespace = "")]
    public class ReferralWebsite
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
}