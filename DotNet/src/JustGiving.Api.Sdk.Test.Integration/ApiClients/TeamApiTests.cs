using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Event;
using JustGiving.Api.Sdk.Model.Team;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class TeamApiTests
    {
    	private JustGivingClient _client;
    	private TeamApi _api;

    	[SetUp]
		public void SetUp()
		{
			WithAValidClient();
		}

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Create_ValidEvent_ReturnsEventRegistrationResponse(WireDataFormat format)
        {
			WithAValidClient(format);

        	var team = new Team
        	             	{
        	             		Name = "My Awesome event",
								Target = 1000,
								Story = "My kick ass story",
								TargetType = TeamTargetType.Fixed.ToString(),
								TeamShortName = "my-great-team" + Guid.NewGuid(),
								TeamType = TeamType.ByInvitationOnly.ToString()
        	             	};

			var eventRegistrationResponse = _api.CreateOrUpdate(team);

			Assert.That(eventRegistrationResponse.Id, Is.Not.EqualTo(0));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		public void TeamExists_AndATeamExists_ReturnsTrue(WireDataFormat format)
        {
			WithAValidClient(format);

        	var team = new Team
        	             	{
        	             		Name = "My Awesome event",
								Target = 1000,
								Story = "My kick ass story",
								TargetType = TeamTargetType.Fixed.ToString(),
								TeamShortName = Guid.NewGuid().ToString(),
								TeamType = TeamType.ByInvitationOnly.ToString()
        	             	};

			_api.CreateOrUpdate(team);

			var existsResponse = _api.TeamExists(team.TeamShortName);

			Assert.That(existsResponse, Is.True);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		public void TeamExists_AndATeamDoesntExist_ReturnsFalse(WireDataFormat format)
        {
			WithAValidClient(format);
			
			var existsResponse = _api.TeamExists(Guid.NewGuid().ToString());

			Assert.That(existsResponse, Is.False);
        }
		
		[TestCase(WireDataFormat.Json)]
		[TestCase(WireDataFormat.Xml)]
		public void Retrieve_AndATeamExists_ReturnsTeam(WireDataFormat format)
		{
			WithAValidClient(format);

			var team1 = new Team
			{
				Name = "My Awesome event",
				Target = 1000,
				Story = "My kick ass story",
				TargetType = TeamTargetType.Fixed.ToString(),
				TeamShortName = Guid.NewGuid().ToString(),
				TeamType = TeamType.ByInvitationOnly.ToString()
			};

			var teamId = _api.CreateOrUpdate(team1).Id;

			var team2 = _api.Retrieve(team1.TeamShortName);

			Assert.That(team2.Id, Is.EqualTo(teamId));
		}

    	private void WithAValidClient(WireDataFormat format = WireDataFormat.Xml)
    	{
    		_client = TestContext.CreateClientValidCredentials(format);
    		_api = new TeamApi(_client.HttpChannel);
    	}
    }
}
