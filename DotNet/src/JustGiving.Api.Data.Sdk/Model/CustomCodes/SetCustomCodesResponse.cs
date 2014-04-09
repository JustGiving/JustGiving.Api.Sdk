using System.Runtime.Serialization;
using JustGiving.Api.Data.Sdk.Model.Payment;

namespace JustGiving.Api.Data.Sdk.Model.CustomCodes
{
    [DataContract(Namespace = "")]
    public class SetCustomCodesResponse : DtoBase
    {
        [DataMember(Name = "rel")]
        public string Rel { get; set; }

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }
    }

    [DataContract(Namespace = "")]
    public class SetCustomCodesForPageResponse : SetCustomCodesResponse
    {
    }

    [DataContract(Namespace = "")]
    public class SetCustomCodesForEventResponse : SetCustomCodesResponse
    {
    }

}