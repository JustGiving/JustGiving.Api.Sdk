using System;
using JustGiving.Api.Sdk.Model.Event;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class EventApi: ApiClientBase, IEventApi
	{
		public override string ResourceBase
		{
			get { return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event"; }
		}

        public EventApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        public string RetrieveLocationFormat(int eventId)
        {
			return ResourceBase + "/" + eventId;
        }

        public Event Retrieve(int eventId)
        {
            var locationFormat = RetrieveLocationFormat(eventId);
        	return Get<Event>(locationFormat);
        }

        public void RetrieveAsync(int eventId, Action<Event> callback)
        {
            var locationFormat = RetrieveLocationFormat(eventId);
            GetAsync(locationFormat, callback);
        }

        public string RetrievePagesLocationFormat(int eventId, int? pageSize, int? pageNumber)
        {
			var locationFormat = ResourceBase + "/" + eventId + "/pages";
            locationFormat += "?PageSize=" + pageSize.GetValueOrDefault(50);
            locationFormat += "&PageNum=" + pageNumber.GetValueOrDefault(1);
            return locationFormat;
        }

        public GetPagesForEventResponse RetrievePages(int eventId, int? pageSize, int? pageNumber)
        {
            var locationFormat = RetrievePagesLocationFormat(eventId, pageSize, pageNumber);
			return Get<GetPagesForEventResponse>(locationFormat);
        }

        public void RetrievePagesAsync(int eventId, int? pageSize, int? pageNumber, Action<GetPagesForEventResponse> callback)
        {
            var locationFormat = RetrievePagesLocationFormat(eventId, pageSize, pageNumber);
			GetAsync(locationFormat, callback);
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