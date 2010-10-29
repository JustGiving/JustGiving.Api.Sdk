using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    [TestFixture]
    public class EventApiTests : ApiClientTestsBase
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveEvent_IssuedWithKnownId_ReturnsEvent(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var eventApi = new EventApi(client);

            var item = eventApi.Retrieve(479546);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePages_IssuedWithKnownId_ReturnsPages(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var eventApi = new EventApi(client);

            var pages = eventApi.RetrievePages(479546);
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePages_IssuedWithKnownIdAndPage2_ReturnsPages(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var eventApi = new EventApi(client);

            var pages = eventApi.RetrievePages(479546, 20, 2);
        }
    }
}
