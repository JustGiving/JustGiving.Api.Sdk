using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model;

namespace JustGiving.Api.Sdk.ApiClients
{
	[DataContract(Namespace = "", Name = "teamCreated")]
	public class TeamCreatedResponse
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