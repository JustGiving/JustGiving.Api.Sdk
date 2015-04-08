using JustGiving.Api.Sdk.Model.Search;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ISearchApi : ISearchApiAsync
    {
        CharitySearchResults CharitySearch(string searchTerms);
        CharitySearchResults CharitySearch(string searchTerms, int? pageNumber, int? pageSize);
        EventSearchResults EventSearch(string searchTerms);
        EventSearchResults EventSearch(string searchTerms, int? pageNumber, int? pageSize);
        InMemorySearchResults InMemorySearch(int? id, string firstName, string lastName, string town);
        InMemorySearchResults InMemorySearch(int? id, string firstName, string lastName, string town, int? pageNumber, int? pageSize);

        SearchApi.FundraiserSearchResponse FundraiserSearch(string query, int? pageNumber = null, int? pageSize = null,
                                                            int? causeId = null,
                                                            int? eventId = null, int? charityId = null,
                                                            int? designId = null);
    }
}