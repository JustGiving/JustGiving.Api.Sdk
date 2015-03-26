using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Account
{
    [DataContract(Namespace = "", Name = "account")]
    public class AccountDetails
    {
        [DataMember(Name = "activePageCount", IsRequired = true)]
        public int ActivePageCount { get; set; }

        [DataMember(Name = "completedPagesCount", IsRequired = true)]
        public int CompletedPagesCount { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }

        [DataMember(Name = "firstName", IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(Name = "joinDate")]
        public DateTime JoinDate { get; set; }

        [DataMember(Name = "lastName", IsRequired = true)]
        public string LastName { get; set; }

        [DataMember(Name = "profileImageUrls")]
        public IDictionary<string, string> ProfileImageUrls { get; set; }

        [DataMember(Name = "totalDonated", IsRequired = true)]
        public decimal TotalDonated { get; set; }

        [DataMember(Name = "totalDonatedGiftAid", IsRequired = true)]
        public decimal TotalDonatedGiftAid { get; set; }

        [DataMember(Name = "totalGiftAid", IsRequired = true)]
        public decimal TotalGiftAid { get; set; }

        [DataMember(Name = "totalRaised", IsRequired = true)]
        public decimal TotalRaised { get; set; }

        [DataMember(Name = "town")]
        public string Town { get; set; }

        [DataMember(Name = "donationTotalsInSupportedCurrencies")]
        public List<MonetaryAmount> DonationTotalsInSupportedCurrencies { get; set; }

        [DataMember(Name = "raisedTotalsInSupportedCurrencies")]
        public List<MonetaryAmount> RaisedTotalsInSupportedCurrencies { get; set; }
    }

    [DataContract]
    public class MonetaryAmount
    {
        [DataMember(Name = "currencySymbol")]
        public string CurrencySymbol { get; set; }
        [DataMember(Name = "currencyCode")]
        public string CurrencyCode { get; set; }
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }
    }
}