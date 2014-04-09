using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.CustomCodes
{
    [DataContract(Namespace = "")]
    public class PageCustomCodes //: ICanCreateMyOwnExample<PageCustomCodes>
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

        public PageCustomCodes ValidExample()
        {
            return new PageCustomCodes
            {
                CustomCode1 = "Foo",
                CustomCode2 = "Bar",
                CustomCode3 = "Nigel's team",
                CustomCode4 = "1943280",
                CustomCode5 = "",
                CustomCode6 = "£12.05"
            };
        }
    }
}