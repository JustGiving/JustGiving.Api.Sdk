using System;
using JustGiving.Api.Sdk.Model.Search;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class SearchApi : ApiClientBase, ISearchApi
    {
        public SearchApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        public CharitySearchResults CharitySearch(string searchTerms)
        {
            return CharitySearch(searchTerms, null, null);
        }

        public CharitySearchResults CharitySearch(string searchTerms, int? pageNumber, int? pageSize)
        {
            if (string.IsNullOrEmpty(searchTerms))
                return new CharitySearchResults();

            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/charity/search";
            locationFormat += "?q=" + Uri.EscapeDataString(searchTerms);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(50);

            return Parent.HttpChannel.PerformApiRequest<CharitySearchResults>("GET", locationFormat);
        }

        public object EventSearch(string searchTerms)
        {
            return EventSearch(searchTerms, null, null);
        }

        public object EventSearch(string searchTerms, int? pageNumber, int? pageSize)
        {
            if (string.IsNullOrEmpty(searchTerms))
                return new CharitySearchResults();

            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/search";
            locationFormat += "?q=" + Uri.EscapeDataString(searchTerms);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(50);

            return Parent.HttpChannel.PerformApiRequest<CharitySearchResults>("GET", locationFormat);
        }
    }
}
