using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "EventCustomCodes", Namespace = "")]
    public class EventCustomCodes
    {
        [DataMember(Order = 10)]
        public string CustomCode1 { get; set; }

        [DataMember(Order = 20)]
        public string CustomCode2 { get; set; }

        [DataMember(Order = 30)]
        public string CustomCode3 { get; set; }
    }
}