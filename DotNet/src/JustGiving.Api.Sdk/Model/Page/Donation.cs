using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "donation", Namespace = "")]
    public abstract class Donation
    {
        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "donorRealName")]
        public string DonorRealName { get; set; }

        [DataMember(Name = "creditCard")]
        public CreditCard CreditCard { get; set; }

        [DataMember(Name = "billingAddress")]
        public Address BillingAddress { get; set; }
    }
}
