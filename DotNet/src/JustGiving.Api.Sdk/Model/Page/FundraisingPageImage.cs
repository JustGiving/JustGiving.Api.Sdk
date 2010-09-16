using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "image", Namespace = "")]
    public class FundraisingPageImage
    {
        [DataMember(Name = "caption")]
        public string Caption { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
