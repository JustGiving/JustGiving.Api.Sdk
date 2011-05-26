using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "addVideo", Namespace="")]
    public class AddFundraisingPageVideoConfirmation
    {
        [DataMember(Name = "next")]
        public RestResponseNextElement Next { get; set; }

        [DataMember(Name = "error")]
        public ErrorResponse Error { get; set; }
    }
}