using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Team
{
	[DataContract(Name = "team", Namespace = "")]
	public class Team
	{
		/// <summary>
		/// Team Id, unused in create / update
		/// </summary>
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public int? Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }
		
		[DataMember(Name = "story")]
		public string Story { get; set; }

		/// <summary>
		/// Team target is a total of member pages: "Aggregate"
		/// Team target is specified value: "Fixed"
		/// </summary>
		[DataMember(Name = "targetType")]
		public string TargetType { get; set; }

		/// <summary>
		/// Team invite mode, one of "Open", "Closed", "ByInvitationOnly"
		/// </summary>
		[DataMember(Name = "teamType")]
		public string TeamType { get; set; }

		/// <summary>
		/// Team target amount used when TargetType is fixed
		/// </summary>
		[DataMember(Name = "target")]
		public decimal Target { get; set; }

		/// <summary>
		/// teamShortName which translates to "/teams/{teamShortName}" on JustGiving.com
		/// </summary>
		[DataMember(Name = "teamShortName", EmitDefaultValue = false)]
		public string TeamShortName { get; set; }

		/// <summary>
		/// Team creation date, automatically generated on the server, unused in create / update
		/// </summary>
		[DataMember(Name = "dateCreated", EmitDefaultValue = false)]
		public DateTime? DateCreated { get; set; }

		/// <summary>
		/// Team total, automatically generated on the server, unused in create / update
		/// </summary>
		[DataMember(Name = "raisedSoFar", EmitDefaultValue = false)]
		public decimal? RaisedSoFar { get; set; }

        [DataMember(Name = "teamMembers", EmitDefaultValue = false)]
        public IList<TeamMember> TeamMembers { get; set; }
	}
}
