using System;
using JustGiving.Api.Sdk.Model.Event;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class EventApi: ApiClientBase, IEventApi
    {
        public EventApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        public string RetrieveLocationFormat(int eventId)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/" + eventId;
        }

        public Event Retrieve(int eventId)
        {
            var locationFormat = RetrieveLocationFormat(eventId);
            return Parent.HttpChannel.PerformApiRequest<Event>("GET", locationFormat);
        }

        public void RetrieveAsync(int eventId, Action<Event> callback)
        {
            var locationFormat = RetrieveLocationFormat(eventId);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public string RetrievePagesLocationFormat(int eventId, int? pageSize, int? pageNumber)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/" + eventId + "/pages";
            locationFormat += "?PageSize=" + pageSize.GetValueOrDefault(50);
            locationFormat += "&PageNum=" + pageNumber.GetValueOrDefault(1);
            return locationFormat;
        }

        public GetPagesForEventResponse RetrievePages(int eventId, int? pageSize, int? pageNumber)
        {
            var locationFormat = RetrievePagesLocationFormat(eventId, pageSize, pageNumber);
            return Parent.HttpChannel.PerformApiRequest<GetPagesForEventResponse>("GET", locationFormat);
        }

        public void RetrievePagesAsync(int eventId, int? pageSize, int? pageNumber, Action<GetPagesForEventResponse> callback)
        {
            var locationFormat = RetrievePagesLocationFormat(eventId, pageSize, pageNumber);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public GetPagesForEventResponse RetrievePages(int eventId)
        {
            return RetrievePages(eventId, 50, 1);
        }

        public void RetrievePagesAsync(int eventId, Action<GetPagesForEventResponse> callback)
        {
            RetrievePagesAsync(eventId, 50, 1, callback);
        }
    }
}