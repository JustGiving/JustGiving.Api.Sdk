using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.OneSearch;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class OneSearchApi : ApiClientBase, IOneSearchApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/onesearch"; }
        }
        public OneSearchApi(HttpChannel channel)
            : base(channel)
        {

        }

        public OneSearchResponse OneSearchIndex(string phraseToSearch, bool groupSearch = false,
            string resultsByIndex = "",
            int limit = 0, int offset = 0, string country = "GB")
        {
            string locationFormat = OneSearchQueryFormat(phraseToSearch, groupSearch, resultsByIndex, limit, offset,
                country);
            return HttpChannel.PerformRequest<OneSearchResponse>("GET", locationFormat);
        }

        public OneSearchResponse OneSearchIndex(string phraseToSearch)
        {
            return OneSearchIndex(phraseToSearch, false, "", 3, 0, "GB");
        }

        private string OneSearchQueryFormat(string phraseToSearch, bool groupSearch,
            string resultsByIndex, int limit, int offset, string country)
        {
            var locationFormat = ResourceBase;
            locationFormat += "?q=" + Uri.EscapeDataString(phraseToSearch ?? string.Empty);
            //locationFormat += "&g=" + groupSearch;
            //locationFormat += "&i=" + resultsByIndex;
            //locationFormat += "&limit=" + limit;
            //locationFormat += "&offset=" + offset;
            locationFormat += "&country=" + country;
            return locationFormat;
        }

        public void OneSearchIndexAsync(string phraseToSearch, Action<OneSearchResponse> callback, bool groupSearch = false, string resultsByIndex = "", int limit = 0, int offset = 0, string country = "GB")
        {
            var locationFormat = OneSearchQueryFormat(phraseToSearch, groupSearch, resultsByIndex, limit, offset,
                country);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public void OneSearchIndexAsync(string phraseToSearch, Action<OneSearchResponse> callback)
        {
            OneSearchIndexAsync(phraseToSearch, callback, false, null, 0, 0);
        }
    }
    public interface IOneSearchApi : IOneSearchApiAsync
    {
        OneSearchResponse OneSearchIndex(string phraseToSearch, bool groupSearch = false, string resultsByIndex = "",
            int limit = 0, int offset = 0, string country = "GB");

        OneSearchResponse OneSearchIndex(string phraseToSearch);
    }

    public interface IOneSearchApiAsync
    {
        void OneSearchIndexAsync(string phraseToSearch, Action<OneSearchResponse> callback, bool groupSearch = false,
            string resultsByIndex = "",
            int limit = 0, int offset = 0, string country = "GB");

        void OneSearchIndexAsync(string phraseToSearch, Action<OneSearchResponse> callback);

    }
}
