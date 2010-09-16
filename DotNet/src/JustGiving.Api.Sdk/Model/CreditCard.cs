using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model
{
    [DataContract(Name = "creditCard", Namespace = "")]
    public class CreditCard
    {
        [DataMember(Name = "cardProvider", IsRequired = true)]
        public string CardProvider { get; set; }

        [DataMember(Name = "holderName", IsRequired = true)]
        public string HolderName { get; set; }

        [DataMember(Name = "cardNumber", IsRequired = true)]
        public string CardNumber { get; set; }

        [DataMember(Name = "cv2", IsRequired = true)]
        public string Cv2 { get; set; }

        [DataMember(Name = "beginMonth", IsRequired = false)]
        public byte? BeginMonth { get; set; }

        [DataMember(Name = "beginYear", IsRequired = false)]
        public short? BeginYear { get; set; }

        [DataMember(Name = "expiryMonth", IsRequired = true)]
        public byte ExpiryMonth { get; set; }

        [DataMember(Name = "expiryYear", IsRequired = true)]
        public short ExpiryYear { get; set; }

        [DataMember(Name = "issue", IsRequired = false)]
        public byte? Issue { get; set; }
    }
}