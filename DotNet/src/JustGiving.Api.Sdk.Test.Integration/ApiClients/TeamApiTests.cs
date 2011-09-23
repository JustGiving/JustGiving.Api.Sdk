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

    	private void WithAValidClient(WireDataFormat format = WireDataFormat.Xml)
    	{
    		_client = TestContext.CreateClientValidCredentials(format);
    		_api = new TeamApi(_client.HttpChannel);
    	}
    }
}
