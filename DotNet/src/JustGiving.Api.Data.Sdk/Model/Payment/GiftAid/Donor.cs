using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.GiftAid
{
    [DataContract(Name = "Donor", Namespace = "")]
    public class Donor
    {
        public Donor()
        {
            Address = new Address();
        }

        [DataMember(Order = 10)]
        public int Id { get; set; }

        [DataMember(Order = 20)]
        public string Title { get; set; }

        [DataMember(Order = 30)]
        public string FirstName { get; set; }

        [DataMember(Order = 40)]
        public string LastName { get; set; }

        [DataMember(Order = 50)]
        public string Email { get; set; }

        [DataMember(Order = 60)]
        public Address Address { get; set; }

        [DataMember(Order = 70)]
        public bool IsFurtherContact { get; set; }

        [DataMember(Order = 80)]
        public bool IsUKTaxPayer { get; set; }

        [DataMember(Order = 90)]
        public bool IsConnected { get; set; }

    }
}