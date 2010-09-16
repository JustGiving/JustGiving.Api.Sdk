using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model
{
    [DataContract(Namespace = "", Name = "navigation")]
    public class RestResponseNavigationElement
    {
        [DataMember(Name = "rel", IsRequired = true, Order = 1)]
        public string Rel { get; set; }

        [DataMember(Name = "uri", IsRequired = true, Order = 2)]
        public string Uri { get; set; }

        [DataMember(Name = "type", IsRequired = true, Order = 3)]
        public string Type { get; set; }
    }
}