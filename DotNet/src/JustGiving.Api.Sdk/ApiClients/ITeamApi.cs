using JustGiving.Api.Sdk.Model.Team;

namespace JustGiving.Api.Sdk.ApiClients
{
	public interface ITeamApi : ITeamApiAsync
	{
		Team Retrieve(string teamShortName);
		TeamCreatedResponse CreateOrUpdate(Team team);
		bool TeamExists(string teamShortName);
	    bool JointTeam(string teamShortName, TeamApi.JoinTeamRequest joinTeamRequest);
	}
}