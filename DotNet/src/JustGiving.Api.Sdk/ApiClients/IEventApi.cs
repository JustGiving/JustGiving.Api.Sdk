using JustGiving.Api.Sdk.Model.Event;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IEventApi
    {
        Event Retrieve(int eventId);
        GetPagesForEventResponse RetrievePages(int eventId);
        GetPagesForEventResponse RetrievePages(int eventId, int? pageSize, int? pageNumber);
    }
}