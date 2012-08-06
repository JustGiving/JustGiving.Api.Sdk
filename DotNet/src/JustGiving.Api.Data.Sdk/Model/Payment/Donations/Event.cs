using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment.Donations
{
    [DataContract(Name = "Event", Namespace = "")]
    public class Event
    {
        public Event()
        {
            CustomCodes = new EventCustomCodes();
        }

        [DataMember(Order = 10)]
        public int? Id { get; set; }

        [DataMember(Order = 20)]
        public string Name { get; set; }

        [DataMember(Order = 30)]
        public DateTime StartDate { get; set; }

        [DataMember(Order = 40)]
        public DateTime ExpiryDate { get; set; }

        [DataMember(Order = 50)]
        public bool IsPromoted { get; set; }

        [DataMember(Order = 60)]
        public string CategoryName { get; set; }

        [DataMember(Order = 70)]
        public bool IsOverSeas { get; set; }

        [DataMember(Order = 80)]
        public bool IsUserCreated { get; set; }

        [DataMember(Order = 90)]
        public EventCustomCodes CustomCodes { get; set; }
    }
}