using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Search
{
    [DataContract(Name = "rememberedPerson", Namespace = "")]
    public class InMemorySearchResult
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "gender")]
        public int? Gender { get; set; }

        [DataMember(Name = "town")]
        public string Town { get; set; }

        [DataMember(Name = "dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "dateOfDeath")]
        public DateTime? DateOfDeath { get; set; }

        [DataMember(Name = "createdBy")]
        public CreatedByPerson CreatedBy { get; set; }
    }

    public class CreatedByPerson
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
    }
}