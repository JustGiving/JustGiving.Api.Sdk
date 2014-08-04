using JustGiving.Api.Sdk.Model.OneSearch;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IOneSearchApi : IOneSearchApiAsync
    {
        OneSearchResponse OneSearchIndex(string phraseToSearch, bool groupSearch = false, string resultsByIndex = "",
            int limit = 0, int offset = 0, string country = "GB");

        OneSearchResponse OneSearchIndex(string phraseToSearch);
    }
}
