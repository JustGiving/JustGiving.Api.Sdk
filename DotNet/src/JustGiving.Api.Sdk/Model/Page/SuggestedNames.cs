using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "SuggestedNames", Namespace = "")]
    public class SuggestedNames
    {
        [DataMember(Name = "Names")]
        public string[] Names { get; set; }
    }
}