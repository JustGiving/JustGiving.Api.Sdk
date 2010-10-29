using JustGiving.Api.Sdk.ApiClients;
using NUnit.Framework;

namespace JustGiving.Api.Sdk.Test.Integration.ApiClients
{
    /// <summary>
    /// Apologies for the fragility of these tests.
    /// Relies on data in the JG dev database. Bad tests, though allow us to
    /// execute pre-release testing
    /// </summary>
    [TestFixture]
    public class EventApiTests : ApiClientTestsBase
    {
        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrieveEvent_IssuedWithKnownId_ReturnsEvent(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var eventApi = new EventApi(client);

            var item = eventApi.Retrieve(479546); // VLM 2011 on local dev
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePages_IssuedWithKnownId_ReturnsPages(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var eventApi = new EventApi(client);

            var pages = eventApi.RetrievePages(479546); // VLM 2011 on local dev
        }

        [TestCase(WireDataFormat.Json)]
        [TestCase(WireDataFormat.Xml)]
        public void RetrievePages_IssuedWithKnownIdAndPage2_ReturnsPages(WireDataFormat format)
        {
            var client = CreateClientNoCredentials(format);
            var eventApi = new EventApi(client);

            var pages = eventApi.RetrievePages(479546, 20, 2); // VLM 2011 on local dev
        }
    }
}
