using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "Payment", Namespace = "")]
    public class Payment : DtoBase
    {
        public Payment()
        {
            Donations = new List<DonationGiftAid>();
        }

        [DataMember(Name = "PaymentRef", Order = 0)]
        public int PaymentRef
        {
            get;
            set;
        }

        [DataMember(Name = "Donations", Order = 1)]
        public List<DonationGiftAid> Donations
        {
            get;
            set;
        }
    }
}