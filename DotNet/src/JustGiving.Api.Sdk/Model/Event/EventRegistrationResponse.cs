using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.Model.Event
{
	[DataContract(Namespace = "", Name = "eventRegistration")]
	public class EventRegistrationResponse
	{
		public string ContentUri { get; set; }

		[DataMember(Name = "next", EmitDefaultValue = false)]
		public RestResponseNextElement Next { get; set; }

		[DataMember(Name = "error", EmitDefaultValue = false)]
		public ErrorResponse Error { get; set; }

		[DataMember(Name = "id", EmitDefaultValue = false)]
		public int Id { get; set; }
	}
}