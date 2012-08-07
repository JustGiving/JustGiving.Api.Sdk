using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "CustomCodes", Namespace = "")]
    public class CustomCodes
    {
        [DataMember(Order = 10)]
        public string CustomCode1 { get; set; }

        [DataMember(Order = 20)]
        public string CustomCode2 { get; set; }

        [DataMember(Order = 30)]
        public string CustomCode3 { get; set; }

        [DataMember(Order = 40)]
        public string CustomCode4 { get; set; }

        [DataMember(Order = 50)]
        public string CustomCode5 { get; set; }

        [DataMember(Order = 60)]
        public string CustomCode6 { get; set; }
    }
}