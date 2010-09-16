using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Account
{
    [DataContract(Namespace = "", Name = "registration")]
    public class CreateAccountRequest
    {
        /// <summary>
        /// Your reference (Optional).
        /// </summary>
        [DataMember(Name = "reference")]
        public string Reference { get; set; }
        /// <summary>
        /// The user's title for example Mr (Required).
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
        /// <summary>
        /// The user's firstName (Required).
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        /// <summary>
        /// The user's lastName (Required).
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "address")]
        public Address Address { get; set; }
        /// <summary>
        /// The user's email (Required).
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }
        /// <summary>
        /// The user's password (Required).
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }
        /// <summary>
        /// A Boolean indicating whether user accepts JustGiving's Terms and conditions. Note providing false will fail validation (Required).
        /// </summary>
        [DataMember(Name = "acceptTermsAndConditions")]
        public bool AcceptTermsAndConditions { get; set; }

        public CreateAccountRequest()
        {
            Address = new Address();
        }
    }
}
