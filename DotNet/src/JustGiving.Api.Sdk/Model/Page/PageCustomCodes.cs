using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "customCodes", Namespace = "")]
    public class PageCustomCodes
    {
        [DataMember(Name = "customCode1", EmitDefaultValue = false, Order = 1)]
        public string CustomCode1 { get; set; }

        [DataMember(Name = "customCode2", EmitDefaultValue = false, Order = 2)]
        public string CustomCode2 { get; set; }

        [DataMember(Name = "customCode3", EmitDefaultValue = false, Order = 3)]
        public string CustomCode3 { get; set; }

        [DataMember(Name = "customCode4", EmitDefaultValue = false, Order = 4)]
        public string CustomCode4 { get; set; }

        [DataMember(Name = "customCode5", EmitDefaultValue = false, Order = 5)]
        public string CustomCode5 { get; set; }

        [DataMember(Name = "customCode6", EmitDefaultValue = false, Order = 6)]
        public string CustomCode6 { get; set; }
    }
}