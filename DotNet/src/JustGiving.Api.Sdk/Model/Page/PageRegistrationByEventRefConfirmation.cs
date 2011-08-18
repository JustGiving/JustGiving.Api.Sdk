using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Namespace = "", Name = "pageRegistration")]
    public class PageRegistrationByEventRefConfirmation : PageRegistrationConfirmation
    {
        [DataMember(Name = "eventId")]
        public int EventId { get; set; }
    }
}