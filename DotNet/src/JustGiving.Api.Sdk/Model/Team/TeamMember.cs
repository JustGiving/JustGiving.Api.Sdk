using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Team
{
    [DataContract(Name = "teamMember", Namespace = "")]
    public class TeamMember
    {
        /// <summary>
        /// pageShortName which translates to "/teams/{pageShortName}" on JustGiving.com
        /// </summary>
        [DataMember(Name = "pageShortName", EmitDefaultValue = false)]
        public string PageShortName { get; set; }
    }
}
