using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Event;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    /// <summary>
    /// Apologies for the fragility of these tests.
    /// Relies on data in the JG dev database. Bad tests, though allow us to
    /// execute pre-release testing
    /// </summary>
    [TestFixture]
    public class EventApiTests
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveEvent_IssuedWithKnownId_ReturnsEvent(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var eventApi = new EventApi(client.HttpChannel);

            eventApi.Retrieve(479546); // VLM 2011 on local dev
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePages_IssuedWithKnownId_ReturnsPages(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var eventApi = new EventApi(client.HttpChannel);

            eventApi.RetrievePages(479546); // VLM 2011 on local dev
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePages_IssuedWithKnownIdAndPage2_ReturnsPages(WireDataFormat format)
        {
            var client = TestContext.CreateClientNoCredentials(format);
			var eventApi = new EventApi(client.HttpChannel);

            eventApi.RetrievePages(479546, 20, 2); // VLM 2011 on local dev
        }


        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void Create_ValidEvent_ReturnsEventRegistrationResponse(WireDataFormat format)
        {
            var client = TestContext.CreateClientValidCredentials(format);
			var eventApi = new EventApi(client.HttpChannel);

        	var @event = new Event
        	             	{
        	             		Name = "My Awesome event",
        	             		StartDate = DateTime.Now,
        	             		CompletionDate = DateTime.Now,
        	             		ExpiryDate = DateTime.Now.AddYears(5),
        	             		Description = "I'm awesome",
        	             		EventType = EventType.OtherCelebration.ToString()
        	             	};

        	var eventRegistrationResponse = eventApi.Create(@event);

			Assert.That(eventRegistrationResponse.Id, Is.Not.EqualTo(0));
        }
    }
}
