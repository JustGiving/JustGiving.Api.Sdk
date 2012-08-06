using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "Charges", Namespace = "")]
    public class Charges
    {
        [DataMember(Order = 10)]
        public string CardProcessFeeRate { get; set; }

        [DataMember(Order = 20)]
        public decimal CardProcessFeeAmount { get; set; }

        [DataMember(Order = 30)]
        public decimal JustGivingTransactionFeeRate { get; set; }

        [DataMember(Order = 40)]
        public decimal JustGivingTransactionFeeAmount { get; set; }
    }
}