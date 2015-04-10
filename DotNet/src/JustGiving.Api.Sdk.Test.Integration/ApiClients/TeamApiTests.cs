using System;
using System.Collections.Generic;

using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Event;
using JustGiving.Api.Sdk.Model.Page;
using JustGiving.Api.Sdk.Model.Team;
using JustGiving.Api.Sdk.Test.Common.Configuration;
using JustGiving.Api.Sdk.Test.Integration.Configuration;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class TeamApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Create_ValidEvent_ReturnsEventRegistrationResponse(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var teamResources = new TeamApi(client.HttpChannel);
            var fundraiseResources = new PageApi(client.HttpChannel);
            var validRegisterPageRequest = ValidRegisterPageRequest();
            var validRequest = ValidTeamRequest(validRegisterPageRequest.PageShortName);
            fundraiseResources.Create(validRegisterPageRequest);

            //act
            var result = teamResources.CreateOrUpdate(validRequest);
            
            //assert
            Assert.That(result.Id, Is.Not.EqualTo(0));
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		public void TeamExists_AndATeamExists_ReturnsTrue(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var teamResources = new TeamApi(client.HttpChannel);
        	var team = new Team
        	             	{
        	             		Name = "My Awesome event",
								Target = 1000.0m,
								Story = "My kick ass story",
								TargetType = TeamTargetType.Fixed.ToString(),
								TeamShortName = Guid.NewGuid().ToString(),
                                TeamType = TeamType.ByInvitationOnly.ToString(),
                                TeamMembers = new List<TeamMember> { new TeamMember { PageShortName = "api-test-c166ab39-fc80-4cf1-aed0-9236974740ca" } }
        	             	};

            teamResources.CreateOrUpdate(team);

            //act
            var result = teamResources.TeamExists(team.TeamShortName);

            //act
            Assert.That(result, Is.True);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
		public void TeamExists_AndATeamDoesntExist_ReturnsFalse(WireDataFormat format)
        {
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var teamResources = new TeamApi(client.HttpChannel);
			
            //act
            var result = teamResources.TeamExists(Guid.NewGuid().ToString());

            //act
            Assert.That(result, Is.False);
        }
		
		[TestCase(WireDataFormat.Json)]
		[TestCase(WireDataFormat.Xml)]
		public void Retrieve_AndATeamExists_ReturnsTeam(WireDataFormat format)
		{
            //arrange
            var client = TestContext.CreateClientValidCredentials(format);
            var teamResources = new TeamApi(client.HttpChannel);
			var team1 = new Team
			{
				Name = "My Awesome event",
				Target = 1000.00m,
				Story = "My kick ass story",
				TargetType = TeamTargetType.Fixed.ToString(),
				TeamShortName = Guid.NewGuid().ToString(),
                TeamType = TeamType.ByInvitationOnly.ToString(),
                TeamMembers = new List<TeamMember> { new TeamMember { PageShortName = "api-test-c166ab39-fc80-4cf1-aed0-9236974740ca" } }
			};
            var teamId = teamResources.CreateOrUpdate(team1).Id;

            //act
            var team2 = teamResources.Retrieve(team1.TeamShortName);

            //assert
			Assert.That(team2.Id, Is.EqualTo(teamId));
		}

        private static RegisterPageRequest ValidRegisterPageRequest()
        {
            return new RegisterPageRequest
            {
                ActivityType = null,
                PageShortName = "test-frp-" + Guid.NewGuid(),
                PageTitle =
                    "When Provided With Valid Authentication Details And An Empty Activity Type - Creates New Page",
                EventName = "The Other Occasion of ApTest and APITest",
                CharityId = 2050,
                EventId = TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.ValidEventId),
                TargetAmount = 20M,
                EventDate = DateTime.Now.AddDays(5)
            };
        }

        private static Team ValidTeamRequest(string pageShortName)
        {
            return new Team
                {
                    Name = "My Awesome event",
                    Target = 1000.00m,
                    Story = "My kick ass story",
                    TargetType = TeamTargetType.Fixed.ToString(),
                    TeamShortName = "my-great-team" + Guid.NewGuid(),
                    TeamType = TeamType.Open.ToString(),
                    TeamMembers =
                        new List<TeamMember>
                            {
                                new TeamMember {PageShortName = pageShortName}
                            }
                };
        }
    }
}
