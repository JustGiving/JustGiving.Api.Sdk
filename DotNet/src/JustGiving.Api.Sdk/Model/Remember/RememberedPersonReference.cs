using System;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Model.Summaries;

namespace JustGiving.Api.Sdk.Model.Remember
{
    [DataContract(Name = "rememberedPersonReference", Namespace = "")]
    public class RememberedPersonReference
    {
        [DataMember(Name = "relationship")]
        public string Relationship { get; set; }

        [DataMember(Name = "rememberedPerson")]
        public RememberedPerson RememberedPerson { get; set; }

    }

    [DataContract(Name = "rememberedPersonResponse", Namespace = "")]
    public class RememberedPersonResponse
    {
        [DataMember(Name = "rememberedPerson")]
        public RememberedPerson RememberedPerson { get; set; }

        [DataMember(Name = "collectionUri")]
        public string CollectionUri { get; set; }
    }
        
    [DataContract(Name = "rememberedPerson", Namespace = "")]
    public class RememberedPerson
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
    }

    [DataContract(Name = "rememberedPersonCollection", Namespace = "")]
    public class RememberedPersonCollection
    {
        [DataMember(Name = "grandTotal")]
        public decimal GrandTotal { get; set; }

        [DataMember(Name = "pages")]
        public PageSummary[] Pages { get; set; }

        [DataMember(Name = "charities")]
        public CharitySummary[] Charities { get; set; }

        [DataMember(Name = "events")]
        public EventSummary[] Events { get; set; }

    }

    [DataContract(Name = "rememberedPersonSummary", Namespace = "")]
    public class RememberedPersonSummary
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "next")]
        public RestResponseNextElement Next { get; set; }

    }
}
