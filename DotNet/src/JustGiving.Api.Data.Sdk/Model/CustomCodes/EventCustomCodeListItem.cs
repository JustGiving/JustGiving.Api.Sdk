using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.CustomCodes
{
    [DataContract(Name = "EventCustomCodes")]
    public class EventCustomCodesListItem 
    {
        /// <summary>
        /// Numeric id of the fundraising event.
        /// </summary>
        [DataMember]
        public int EventId { get; set; }

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

        public EventCustomCodesListItem ValidExample()
        {
            return new EventCustomCodesListItem
            {
                EventId = 3243245,
                CustomCode1 = "Euro",
                CustomCode2 = "Frac",
                CustomCode3 = "5U"
            };
        }
    }
}