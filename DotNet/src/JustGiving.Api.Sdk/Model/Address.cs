using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model
{
    [DataContract(Namespace = "", Name = "address")]
    public class Address
    {
        /// <summary>
        /// The first line of the of the address where the user resides (Required).
        /// </summary>
        [DataMember(Name = "line1")]
        public string Line1 { get; set; }
        /// <summary>
        /// The second line of the of the address where the user resides (Optional).
        /// </summary>
        [DataMember(Name = "line2")]
        public string Line2 { get; set; }
        /// <summary>
        /// The town or city where the user resides (Required).
        /// </summary>
        [DataMember(Name = "townOrCity")]
        public string TownOrCity { get; set; }
        /// <summary>
        /// The county or state where the user resides (Required).
        /// </summary>
        [DataMember(Name = "countyOrState")]
        public string CountyOrState { get; set; }
        /// <summary>
        /// The country where the user resides (Required).
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }
        /// <summary>
        /// The postcode or zip of the address where the user resides (Required).
        /// </summary>
        [DataMember(Name = "postcodeOrZipcode")]
        public string PostcodeOrZipcode { get; set; }
    }
}