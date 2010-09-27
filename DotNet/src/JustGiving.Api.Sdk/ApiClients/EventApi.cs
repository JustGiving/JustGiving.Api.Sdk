using JustGiving.Api.Sdk.Model.Event;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class EventApi: ApiClientBase, IEventApi
    {
        public EventApi(JustGivingClient parent)
            : base(parent)
        {
        }

        public Event Retrieve(int eventId)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/" + eventId;
            return Parent.HttpChannel.PerformApiRequest<Event>("GET", locationFormat);
        }
    }
}