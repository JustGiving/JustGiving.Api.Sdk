using System;
using JustGiving.Api.Sdk.Model.Search;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ISearchApiAsync
    {
        void CharitySearchAsync(string searchTerms, Action<CharitySearchResults> callback);
        void CharitySearchAsync(string searchTerms, int? pageNumber, int? pageSize, Action<CharitySearchResults> callback);
    }
}