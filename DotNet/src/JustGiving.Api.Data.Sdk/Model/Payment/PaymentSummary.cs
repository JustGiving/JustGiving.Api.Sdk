using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment
{
    [DataContract(Name = "Payment")]
    public class PaymentSummary
    {
        /// <summary>
        /// The payment reference.
        /// </summary>
        [DataMember]
        public int PaymentRef { get; set; }

        /// <summary>
        /// The date of the payment.
        /// </summary>
        [DataMember]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// The account the payment was made to.
        /// </summary>
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// The API URL of the details of the payment
        /// </summary>
        [DataMember]
        public string Url { get; set; }

        /// <summary>
        /// The type (donation or Gift Aid) of the payment.
        /// </summary>
        [DataMember]
        public virtual PaymentType PaymentType { get; set; }

        /// <summary>
        /// Gross payment amount.
        /// </summary>
        [DataMember]
        public virtual double Gross { get; set; }

        /// <summary>
        /// Commission fee.
        /// </summary>
        [DataMember]
        public virtual double Commission { get; set; }

        /// <summary>
        /// Card processing fee.
        /// </summary>
        [DataMember]
        public virtual double CardProcessingFee { get; set; }

        /// <summary>
        /// VAT.
        /// </summary>
        [DataMember]
        public virtual double VAT { get; set; }

        /// <summary>
        /// Net payment amount.
        /// </summary>
        [DataMember]
        public virtual double Net { get; set; }             
    }

    public enum PaymentType
    {
        donation, giftaid
    }
}