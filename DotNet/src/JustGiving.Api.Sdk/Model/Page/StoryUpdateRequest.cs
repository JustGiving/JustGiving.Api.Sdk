using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "fundraisingPage", Namespace = "")]
    public class StoryUpdateRequest
    {
        [DataMember(Name = "storySupplement", EmitDefaultValue = false)]
        public string StorySupplement { get; set; }
    }
}