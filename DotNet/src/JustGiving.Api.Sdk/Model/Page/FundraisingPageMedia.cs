using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "media", Namespace = "")]
    public class FundraisingPageMedia
    {
        [DataMember(Name = "images", EmitDefaultValue = false)]
        public FundraisingPageImage[] Images { get; set; }

        [DataMember(Name = "videos", EmitDefaultValue = false)]
        public FundraisingPageVideo[] Videos { get; set; }
    }
}