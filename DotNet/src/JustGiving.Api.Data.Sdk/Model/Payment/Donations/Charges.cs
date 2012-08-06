using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "Charges", Namespace = "")]
    public class Charges
    {
        [DataMember(Order = 10)]
        public string PaymentProcessFeeRate { get; set; }

        [DataMember(Order = 20)]
        public decimal PaymentProcessFeeAmount { get; set; }

        [DataMember(Order = 30)]
        public decimal JustGivingTransactionFeeRate { get; set; }

        [DataMember(Order = 40)]
        public decimal JustGivingTransactionFeeAmount { get; set; }

        [DataMember]
        public string SmsOperator { get; set; }

        [DataMember]
        public double SmsOperatorDonorTransactionFee { get; set; }

        [DataMember]
        public double DonationNetAmountPaid { get; set; }

        [DataMember]
        public double JustGivingTransactionFeePaid { get; set; }

        [DataMember]
        public double ProcessingFeePaid { get; set; }

        [DataMember]
        public string CommissionPayer { get; set; }

        [DataMember]
        public double NetTotalChargesByJustGiving { get; set; }
    }
}