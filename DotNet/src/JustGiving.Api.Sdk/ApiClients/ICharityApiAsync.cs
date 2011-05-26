using System;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ICharityApiAsync
    {
        void RetrieveAsync(int charityId, Action<Charity> callback);
        void RetrieveEventsAsync(int charityId, Action<CharityEvents> callback);
        void RetrieveEventsAsync(int charityId, int pageNumber, int pageSize, Action<CharityEvents> callback);
    }
}