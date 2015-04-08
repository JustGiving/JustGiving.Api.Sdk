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

        public CharitySearchResults CharitySearch(string searchTerms)
        {
            return CharitySearch(searchTerms, null, null);
        }

        public CharitySearchResults CharitySearch(string searchTerms, int? pageNumber, int? pageSize)
        {
            string locationFormat = CharitySearchLocationFormat(searchTerms, pageNumber, pageSize);
            return HttpChannel.PerformRequest<CharitySearchResults>("GET", locationFormat);
        }

        public void CharitySearchAsync(string searchTerms, Action<CharitySearchResults> callback)
        {
            CharitySearchAsync(searchTerms, null, null, callback);
        }

        public void CharitySearchAsync(string searchTerms, int? pageNumber, int? pageSize, Action<CharitySearchResults> callback)
        {
            var locationFormat = CharitySearchLocationFormat(searchTerms, pageNumber, pageSize);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public EventSearchResults EventSearch(string searchTerms)
        {
            return EventSearch(searchTerms, null, null);
        }

        public EventSearchResults EventSearch(string searchTerms, int? pageNumber, int? pageSize)
        {
            if (string.IsNullOrEmpty(searchTerms))
                return new EventSearchResults();

			var locationFormat = EventSearchLocationFormat(searchTerms, pageNumber, pageSize);
            return HttpChannel.PerformRequest<EventSearchResults>("GET", locationFormat);
        }

        public void EventSearchAsync(string searchTerms, Action<EventSearchResults> callback)
        {
            EventSearchAsync(searchTerms, null, null, callback);
        }

        public void EventSearchAsync(string searchTerms, int? pageNumber, int? pageSize, Action<EventSearchResults> callback)
        {
            if (string.IsNullOrEmpty(searchTerms))
                callback(new EventSearchResults());

            var locationFormat = EventSearchLocationFormat(searchTerms, pageNumber, pageSize);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        
        public InMemorySearchResults InMemorySearch(int? id, string firstName, string lastName, string town)
        {
            return InMemorySearch(id, firstName, lastName, town, null, null);
        }

        public InMemorySearchResults InMemorySearch(int? id, string firstName, string lastName, string town, int? pageNumber, int? pageSize)
        {
            var locationFormat = InMemorySearchLocationFormat(id, firstName, lastName, town, pageNumber, pageSize);
            return HttpChannel.PerformRequest<InMemorySearchResults>("GET", locationFormat);
        }

        public void InMemorySearchAsync(int? id, string firstName, string lastName, string town, Action<InMemorySearchResults> callback)
        {
            InMemorySearchAsync(id, firstName, lastName, town, null, null, callback);
        }

        public void InMemorySearchAsync(int? id, string firstName, string lastName, string town, int? pageNumber, int? pageSize, Action<InMemorySearchResults> callback)
        {
            var locationFormat = InMemorySearchLocationFormat(id, firstName, lastName, town, pageNumber, pageSize);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        private string CharitySearchLocationFormat(string searchTerms, int? pageNumber, int? pageSize)
        {
            var locationFormat = ResourceBase;
            locationFormat += "?q=" + Uri.EscapeDataString(searchTerms ?? string.Empty);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(20);
            return locationFormat;
        }

        private string EventSearchLocationFormat(string searchTerms, int? pageNumber, int? pageSize)
        {
            var locationFormat = CharitySearchLocationFormat(searchTerms, pageNumber, pageSize);
            locationFormat = locationFormat.Replace("/charity/", "/event/");
            return locationFormat;
        }

        private string InMemorySearchLocationFormat(int? id, string firstName, string lastName, string town, int? pageNumber, int? pageSize)
        {
            var locationFormat = ResourceBase;
            locationFormat += "?id=" + (id==null ? string.Empty : id.ToString());
            locationFormat += "&firstName=" + Uri.EscapeDataString(firstName ?? string.Empty);
            locationFormat += "&lastName=" + Uri.EscapeDataString(lastName ?? string.Empty);
            locationFormat += "&town=" + Uri.EscapeDataString(town ?? string.Empty);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(20);
            locationFormat = locationFormat.Replace("/charity/", "/remember/");
            return locationFormat;
        }
    }
}
