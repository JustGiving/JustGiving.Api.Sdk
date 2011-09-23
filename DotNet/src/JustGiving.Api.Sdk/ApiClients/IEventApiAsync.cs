using System;
using JustGiving.Api.Sdk.Model.Event;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IEventApiAsync
    {
        void RetrieveAsync(int eventId, Action<Event> callback);
        void RetrievePagesAsync(int eventId, Action<GetPagesForEventResponse> callback);
        void RetrievePagesAsync(int eventId, int? pageSize, int? pageNumber, Action<GetPagesForEventResponse> callback);
    	void Create(Event @event, Action<Event> callback);
    }
}