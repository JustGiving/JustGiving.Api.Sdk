using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using JustGiving.Api.Data.Sdk.Model.Payment.GiftAid;

namespace JustGiving.Api.Data.Sdk.Model.Payment
{
    //[DataContract(Name = "Payment")]
    //public class Payment : DtoBase
    //{
    //    public Payment()
    //    {
    //        Donations = new List<Donation>();
    //    }

    //    [DataMember(Name = "PaymentRef", Order = 0)]
    //    public int PaymentRef
    //    {
    //        get;
    //        set;
    //    }

    //    [DataMember(Name = "Donations", Order = 1)]
    //    public List<Donation> Donations
    //    {
    //        get;
    //        set;
    //    }
    //}

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

    [DataContract(Namespace = "", Name = "dtoBase")]
    public abstract class DtoBase
    {
        protected DtoBase()
        {
            HttpStatusCode = HttpStatusCode.OK;
            StatusSummary = "OK";
        }

        public virtual HttpStatusCode HttpStatusCode { get; set; }
        public virtual string StatusSummary { get; set; }

        public void SetStatusCode(HttpStatusCode statusCode, string statusSummary = "")
        {
            HttpStatusCode = statusCode;
            StatusSummary = statusSummary;
        }
    }
}