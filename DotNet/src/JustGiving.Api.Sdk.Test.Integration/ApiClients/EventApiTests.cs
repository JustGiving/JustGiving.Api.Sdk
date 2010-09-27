using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class EventApiTests
    {
        [Test]
        public void RetrieveEvent_IssuedWithKnownId_ReturnsEvent()
        {
            var client = new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, 1));
            var eventApi = new EventApi(client);

            var item = eventApi.Retrieve(479546);
        }
    }
}
