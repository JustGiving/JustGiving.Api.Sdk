using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Charity
{
    [DataContract(Name="charity", Namespace = "")]
    public class CharityEvents
    {
        [DataMember(Name="events", EmitDefaultValue = false)]
        public List<Event.Event> Events { get; set; }

        [DataMember(Name="pagination", EmitDefaultValue = false)]
        public Pagination Pagination { get; set; }

        [DataMember(Name="id", EmitDefaultValue = false)]
        public int CharityId { get; set; }
    }
}
