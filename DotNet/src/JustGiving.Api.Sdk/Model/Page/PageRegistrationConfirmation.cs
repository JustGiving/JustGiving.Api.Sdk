using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Namespace = "", Name = "pageRegistration")]
    public class PageRegistrationConfirmation
    {
        public static string ContentUri = "fundraising/pages";

        public PageRegistrationConfirmation()
        {
        }

        public PageRegistrationConfirmation(string contentRel, string contentUri)
            : this()
        {
            Next = new RestResponseNextElement { Type = "application/html", Rel = contentRel, Uri = contentUri };
        }

        [DataMember(Name = "next")]
        public RestResponseNextElement Next { get; set; }

        [DataMember(Name = "error")]
        public ErrorResponse Error { get; set; }
    }
}
