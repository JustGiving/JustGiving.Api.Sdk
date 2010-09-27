using JustGiving.Api.Sdk.Model.Event;
using JustGiving.Api.Sdk.Model.Page;

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

        public GetPagesForEventResponse RetrievePages(int eventId)
        {
            return RetrievePages(eventId, 50, 1);
        }

        public GetPagesForEventResponse RetrievePages(int eventId, int? pageSize, int? pageNumber)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/" + eventId + "/pages";
            locationFormat += "?PageSize=" + pageSize.GetValueOrDefault(50);
            locationFormat += "&PageNum=" + pageNumber.GetValueOrDefault(1);

            return Parent.HttpChannel.PerformApiRequest<GetPagesForEventResponse>("GET", locationFormat);
        }
    }
}