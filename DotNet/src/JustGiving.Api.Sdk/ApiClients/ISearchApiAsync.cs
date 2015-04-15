using System;
using JustGiving.Api.Sdk.Model.Search;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ISearchApiAsync
    {
        void CharitySearchAsync(string searchTerms, Action<CharitySearchResults> callback);
        void CharitySearchAsync(string searchTerms, int? pageNumber, int? pageSize, Action<CharitySearchResults> callback);
        void EventSearchAsync(string searchTerms, Action<EventSearchResults> callback);
        void EventSearchAsync(string searchTerms, int? pageNumber, int? pageSize, Action<EventSearchResults> callback);
        void InMemorySearchAsync(int? id, string firstName, string lastName, string town, Action<InMemorySearchResults> callback);
        void InMemorySearchAsync(int? id, string firstName, string lastName, string town, int? pageNumber, int? pageSize, Action<InMemorySearchResults> callback);

        void FundraiserSearchAsync(string query, Action<SearchApi.FundraiserSearchResponse> callback, int? pageNumber = null,
                              int? pageSize = null,
                              int? causeId = null,
                              int? eventId = null, int? charityId = null,
                              int? designId = null);

        void TeamSearchAsync(string teamName, Action<SearchApi.TeamSearchResponse> callback, string teamShortName = null,
                        int? teamId = null, int? page = null,
                        int? pageSize = null, int? teamMemberPageId = null, int? teamMemberPageShortName = null,
                        int? teamMemberPageOwnerName = null);
    }
}