using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model;
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

        public void CharitySearchAsync(string searchTerms, int? pageNumber, int? pageSize,
                                       Action<CharitySearchResults> callback)
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

        public void EventSearchAsync(string searchTerms, int? pageNumber, int? pageSize,
                                     Action<EventSearchResults> callback)
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

        public InMemorySearchResults InMemorySearch(int? id, string firstName, string lastName, string town,
                                                    int? pageNumber, int? pageSize)
        {
            var locationFormat = InMemorySearchLocationFormat(id, firstName, lastName, town, pageNumber, pageSize);
            return HttpChannel.PerformRequest<InMemorySearchResults>("GET", locationFormat);
        }

        public void InMemorySearchAsync(int? id, string firstName, string lastName, string town,
                                        Action<InMemorySearchResults> callback)
        {
            InMemorySearchAsync(id, firstName, lastName, town, null, null, callback);
        }

        public void InMemorySearchAsync(int? id, string firstName, string lastName, string town, int? pageNumber,
                                        int? pageSize, Action<InMemorySearchResults> callback)
        {
            var locationFormat = InMemorySearchLocationFormat(id, firstName, lastName, town, pageNumber, pageSize);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public void FundraiserSearchAsync(string query, Action<FundraiserSearchResponse> callback, int? pageNumber = null, int? pageSize = null,
                                          int? causeId = null, int? eventId = null, int? charityId = null, int? designId = null)
        {
            var resourceEndpoint = FundraiserSearchLocationFormat(query, pageNumber, pageSize, causeId, eventId,
                                                                  charityId, designId);
            HttpChannel.PerformRequestAsync("GET", resourceEndpoint, callback);
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

        private string InMemorySearchLocationFormat(int? id, string firstName, string lastName, string town,
                                                    int? pageNumber, int? pageSize)
        {
            var locationFormat = ResourceBase;
            locationFormat += "?id=" + (id == null ? string.Empty : id.ToString());
            locationFormat += "&firstName=" + Uri.EscapeDataString(firstName ?? string.Empty);
            locationFormat += "&lastName=" + Uri.EscapeDataString(lastName ?? string.Empty);
            locationFormat += "&town=" + Uri.EscapeDataString(town ?? string.Empty);
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(20);
            locationFormat = locationFormat.Replace("/charity/", "/remember/");
            return locationFormat;
        }

        private string FundraiserSearchLocationFormat(string query, int? pageNumber = null, int? pageSize = null, int? causeId = null,
                                                      int? eventId = null, int? charityId = null, int? designId = null)
        {
            var locationFormat = ResourceBase;
            locationFormat += "?q=" + query;
            locationFormat += "&page=" + pageNumber.GetValueOrDefault(1);
            locationFormat += "&pageSize=" + pageSize.GetValueOrDefault(20);
            locationFormat += "&causeId=" + (causeId.HasValue ? causeId.Value.ToString() : string.Empty);
            locationFormat += "&eventId=" + (eventId.HasValue ? eventId.Value.ToString() : string.Empty);
            locationFormat += "&charityId=" + (charityId.HasValue ? charityId.Value.ToString() : string.Empty);
            locationFormat += "&designId=" + (designId.HasValue ? designId.Value.ToString() : string.Empty);
            locationFormat = locationFormat.Replace("/charity/", "/fundraising/");
            return locationFormat;
        }

        public FundraiserSearchResponse FundraiserSearch(string query, int? pageNumber = null, int? pageSize = null, int? causeId = null,
                                                      int? eventId = null, int? charityId = null, int? designId = null)
        {
            var resourceEndpoint = FundraiserSearchLocationFormat(query, pageNumber, pageSize, causeId, eventId,
                                                                  charityId, designId);
            var result = HttpChannel.PerformRequest<FundraiserSearchResponse>("GET", resourceEndpoint);
            return result;
        }

        [DataContract(Name = "FundraiserSearch", Namespace = "")]
        public class FundraiserSearchResponse
        {
            [DataMember(Name = "SearchResults")]
            public FundraiserSearchResult[] SearchResults { get; set; }

            [DataMember(Name = "prev")]
            public RestResponsePrevElement Prev { get; set; }

            [DataMember(Name = "next")]
            public RestResponseNextElement Next { get; set; }

        }

        [DataContract(Name = "Fundraiser", Namespace = "")]
        public class FundraiserSearchResult
        {
            [DataMember]
            public string PageUrl { get; set; }

            [DataMember]
            public string Photo { get; set; }

            [DataMember]
            public string ImageAbsoluteUrl { get; set; }

            [DataMember]
            public string PageName { get; set; }

            [DataMember]
            public string PageOwner { get; set; }

            [DataMember]
            public string TeamMembers { get; set; }

            [DataMember]
            public DateTime CreatedDate { get; set; }

            [DataMember]
            public int EventId { get; set; }

            [DataMember]
            public int CauseId { get; set; }

            [DataMember]
            public string Status { get; set; }

            [DataMember]
            public int CharityId { get; set; }

            [DataMember]
            public DateTime? EventDate { get; set; }

            [DataMember]
            public int DesignId { get; set; }

            [DataMember]
            public int PercentageTargetAchieved { get; set; }

            [DataMember]
            public decimal TargetAmount { get; set; }

            [DataMember]
            public string EventName { get; set; }
        }
    }

}

