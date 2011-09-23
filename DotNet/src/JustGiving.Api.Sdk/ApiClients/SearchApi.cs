using System;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Search;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class SearchApi : ApiClientBase, ISearchApi
	{
		public override string ResourceBase
		{
			get { return "{apiKey}/v{apiVersion}/charity/search"; }
		}

        public SearchApi(HttpChannel channel)
            : base(channel)
        {
        }

        public string CharitySearchLocationFormat(string searchTerms, int? pageNumber, int? pageSize)
        {
			var locationFormat = ResourceBase;
            locationFormat += "?q=" + Uri.EscapeDataString(searchTerms);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(50);
            return locationFormat;
        }

        public CharitySearchResults CharitySearch(string searchTerms)
        {
            return CharitySearch(searchTerms, null, null);
        }

        public CharitySearchResults CharitySearch(string searchTerms, int? pageNumber, int? pageSize)
        {
            if (string.IsNullOrEmpty(searchTerms))
                return new CharitySearchResults();

            string locationFormat = CharitySearchLocationFormat(searchTerms, pageNumber, pageSize);
            return HttpChannel.PerformRequest<CharitySearchResults>("GET", locationFormat);
        }

        public void CharitySearchAsync(string searchTerms, Action<CharitySearchResults> callback)
        {
            CharitySearchAsync(searchTerms, null, null, callback);
        }

        public void CharitySearchAsync(string searchTerms, int? pageNumber, int? pageSize, Action<CharitySearchResults> callback)
        {
            if (string.IsNullOrEmpty(searchTerms))
                callback(new CharitySearchResults());

            var locationFormat = CharitySearchLocationFormat(searchTerms, pageNumber, pageSize);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public object EventSearch(string searchTerms)
        {
            return EventSearch(searchTerms, null, null);
        }

        public object EventSearch(string searchTerms, int? pageNumber, int? pageSize)
        {
            if (string.IsNullOrEmpty(searchTerms))
                return new CharitySearchResults();

			var locationFormat = ResourceBase.Replace("charity", "event");
            locationFormat += "?q=" + Uri.EscapeDataString(searchTerms);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(50);

            return HttpChannel.PerformRequest<CharitySearchResults>("GET", locationFormat);
        }
    }
}
