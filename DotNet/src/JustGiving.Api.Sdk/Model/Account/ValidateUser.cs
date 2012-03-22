using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Account
{
	[DataContract(Namespace = "", Name = "validateUserRequest")]
	public class ValidateUser
	{
		[DataMember(Name = "email")]
		public string Email { get; set; }

		[DataMember(Name = "password")]
		public string Password { get; set; }
	}
}
