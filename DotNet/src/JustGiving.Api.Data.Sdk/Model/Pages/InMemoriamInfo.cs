using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract(Namespace = "")]
    public class InMemoriamInfo
    {
        [DataMember]
        public bool IsInMemoriam { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Town { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public DateTime? DateOfBirth { get; set; }

        [DataMember]
        public DateTime? DateOfDeath { get; set; }

        [DataMember]
        public string Relationship { get; set; }

        public static implicit operator bool(InMemoriamInfo info)
        {
            return info != null && info.IsInMemoriam;
        }
    }
}