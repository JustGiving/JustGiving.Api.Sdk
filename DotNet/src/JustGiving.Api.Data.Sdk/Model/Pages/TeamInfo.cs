using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Pages
{
    [DataContract(Namespace = "")]
    public class TeamInfo
    {
        [DataMember]
        public string Members { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}