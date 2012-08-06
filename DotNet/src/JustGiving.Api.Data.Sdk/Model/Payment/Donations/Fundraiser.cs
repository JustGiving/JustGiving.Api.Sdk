using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "Fundraiser", Namespace = "")]
    public class Fundraiser
    {
        public Fundraiser()
        {
            Address = new Address();
        }

        [DataMember(Order = 10)]
        public int? UserId { get; set; }

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
    }
}