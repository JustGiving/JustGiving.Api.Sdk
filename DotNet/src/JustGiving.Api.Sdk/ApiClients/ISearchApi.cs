using JustGiving.Api.Sdk.Model.Search;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ISearchApi: ISearchApiAsync
    {
        CharitySearchResults CharitySearch(string searchTerms);
        CharitySearchResults CharitySearch(string searchTerms, int? pageNumber, int? pageSize);
    }
}