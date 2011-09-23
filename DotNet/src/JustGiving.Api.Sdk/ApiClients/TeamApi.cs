using System;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
using JustGiving.Api.Sdk.Model.Team;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class TeamApi: ApiClientBase, ITeamApi
	{
		public override string ResourceBase
		{
			get { return "{apiKey}/v{apiVersion}/team"; }
		}

		public TeamApi(HttpChannel channel)
            : base(channel)
        {
        }

        public string RetrieveLocationFormat(string teamShortName)
        {
			return ResourceBase + "/" + teamShortName;
        }

        public Team Retrieve(string teamShortName)
        {
			var locationFormat = RetrieveLocationFormat(teamShortName);
			return HttpChannel.Get<Team>(locationFormat);
        }

        public void RetrieveAsync(string teamShortName, Action<Team> callback)
        {
            var locationFormat = RetrieveLocationFormat(teamShortName);
			HttpChannel.GetAsync(locationFormat, callback);
        }
		
		public TeamCreatedResponse CreateOrUpdate(Team team)
		{
			return HttpChannel.Put<Team, TeamCreatedResponse>(ResourceBase + "/" + team.TeamShortName, team);
		}

		public void CreateOrUpdate(Team team, Action<TeamCreatedResponse> callback)
		{
			HttpChannel.PutAsync(ResourceBase + "/" + team.TeamShortName, team, callback);
		}

		public bool TeamExists(string teamShortName)
		{
			var locationFormat = RetrieveLocationFormat(teamShortName);
			var response = HttpChannel.PerformRawRequest("HEAD", locationFormat);
			return InspectResponseStatusCode(response);
		}

		public void TeamExistsAsync(string teamShortName, Action<bool> callback)
		{
			var locationFormat = RetrieveLocationFormat(teamShortName);
			HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response =>
			{
				var pageIsRegistered = InspectResponseStatusCode(response);
				callback(pageIsRegistered);
			});
		}

		private static bool InspectResponseStatusCode(HttpResponseMessage response)
		{
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					return true;
				case HttpStatusCode.NotFound:
					return false;
				default:
					throw ErrorResponseExceptionFactory.CreateException(response, null);
			}
		}
    }
}