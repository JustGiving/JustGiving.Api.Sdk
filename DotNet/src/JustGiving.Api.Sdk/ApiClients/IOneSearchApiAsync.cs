using System;
using JustGiving.Api.Sdk.Model.OneSearch;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IOneSearchApiAsync
    {
        void OneSearchIndexAsync(string phraseToSearch, Action<OneSearchResponse> callback, bool groupSearch = false,
              string resultsByIndex = "",
              int limit = 0, int offset = 0, string country = "GB");

        void OneSearchIndexAsync(string phraseToSearch, Action<OneSearchResponse> callback);
    }
}
