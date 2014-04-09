using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.CustomCodes
{
    [DataContract(Namespace = "")]
    public class EventCustomCodes
    {
        /// <summary>
        /// Value of the first custom code. Max 20 characters.
        /// </summary>
        [DataMember]
        [StringLength(20)]
        [RegularExpression(Regex.CustomCode)]
        public string CustomCode1 { get; set; }

        /// <summary>
        /// Value of the second custom code. Max 20 characters.
        /// </summary>
        [DataMember]
        [StringLength(20)]
        [RegularExpression(Regex.CustomCode)]
        public string CustomCode2 { get; set; }

        /// <summary>
        /// Value of the third custom code. Max 20 characters.
        /// </summary>
        [DataMember]
        [StringLength(20)]
        [RegularExpression(Regex.CustomCode)]
        public string CustomCode3 { get; set; }
    }
}