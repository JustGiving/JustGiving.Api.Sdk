using System;
using JustGiving.Api.Sdk.Model.Team;

namespace JustGiving.Api.Sdk.ApiClients
{
	public interface ITeamApiAsync
	{
		void RetrieveAsync(string teamShortName, Action<Team> callback);
		void CreateOrUpdate(Team team, Action<TeamCreatedResponse> callback);
		void TeamExistsAsync(string teamShortName, Action<bool> callback);
	}
}