using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name="addImage", Namespace = "")]
    public class AddFundraisingPageImageConfirmation
    {
        [DataMember(Name = "next")]
        public RestResponseNextElement Next { get; set; }

        [DataMember(Name = "error")]
        public ErrorResponse Error { get; set; }
    }
}