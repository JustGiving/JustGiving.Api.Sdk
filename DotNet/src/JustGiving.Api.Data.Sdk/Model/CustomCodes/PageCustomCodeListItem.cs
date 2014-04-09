using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.CustomCodes
{
    [DataContract(Name = "PageCustomCodes")]
    public class PageCustomCodesListItem
    {
        /// <summary>
        /// Numeric id of the fundraising page.
        /// </summary>
        [DataMember]
        public int PageId { get; set; }

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

        /// <summary>
        /// Value of the fourth custom code. Max 20 characters.
        /// </summary>
        [DataMember]
        [StringLength(20)]
        [RegularExpression(Regex.CustomCode)]
        public string CustomCode4 { get; set; }

        /// <summary>
        /// Value of the fifth custom code. Max 20 characters.
        /// </summary>
        [DataMember]
        [StringLength(20)]
        [RegularExpression(Regex.CustomCode)]
        public string CustomCode5 { get; set; }

        /// <summary>
        /// Value of the sixth custom code. Max 20 characters.
        /// </summary>
        [DataMember]
        [StringLength(20)]
        [RegularExpression(Regex.CustomCode)]
        public string CustomCode6 { get; set; }
    }
}
