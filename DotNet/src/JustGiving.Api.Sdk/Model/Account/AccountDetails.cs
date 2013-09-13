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

        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }

        [DataMember(Name = "totalDonated", IsRequired = true)]
        public decimal TotalDonated { get; set; }

        [DataMember(Name = "totalDonatedGiftAid", IsRequired = true)]
        public decimal TotalDonatedGiftAid { get; set; }

        [DataMember(Name = "totalGiftAid", IsRequired = true)]
        public decimal TotalGiftAid { get; set; }

        [DataMember(Name = "totalRaised", IsRequired = true)]
        public decimal TotalRaised { get; set; }

        [DataMember(Name = "inMemoryPagesCount", IsRequired = true)]
        public int InMemoryPagesCount { get; set; }

        [DataMember(Name = "firstName", IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName", IsRequired = true)]
        public string LastName { get; set; }
    }
}