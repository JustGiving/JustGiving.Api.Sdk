using System;
using JustGiving.Api.Data.Sdk.Model;
using JustGiving.Api.Data.Sdk.Model.Pages;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public class PagesApi : DataApiClientBase, IPagesApi
    {
        public PagesApi(HttpChannel channel)
            : base(channel)
        {
        }

        public override string ResourceBase
        {
            get { return BaseRoot + "/pages/created"; }
        }

        public PagesCreated RetrievePagesCreated(DateTime startDate, DateTime endDate, int eventId = 0)
        {
            var uri = BaseRoot + BuildUrl(startDate, endDate, eventId);
            return HttpChannel.PerformRequest<PagesCreated>("GET", uri);
        }
        
        public byte[] RetrievePagesCreated(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BaseRoot + BuildUrl(startDate, endDate);
            var response = HttpChannel.PerformRawRequest("GET", uri, ContentTypes.GetAcceptContentType(fileFormat));

            return response.Content.Content;
        }

        public PagesCreated Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate)
        {
            var uri = BuildFormatUriForSearch(query, startDate, endDate);
            return HttpChannel.PerformRequest<PagesCreated>("GET", uri);
        }

        public byte[] Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BuildFormatUriForSearch(query, startDate, endDate);
            var response = HttpChannel.PerformRawRequest("GET", uri, ContentTypes.GetAcceptContentType(fileFormat));

            return response.Content.Content;
        }

        private string BuildUrl(DateTime startDate, DateTime endDate, int eventId = 0)
        {
            var uriDatePart = string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", startDate.Date, endDate.Date);
            return eventId == 0 ? "/pages/created" + uriDatePart : "/events/" + eventId + "/pages/created" + uriDatePart;
        }

        private string BuildFormatUriForSearch(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate)
        {
            return ResourceBase + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}/search?{2}", startDate.Date, endDate.Date, query.GetQueryStringPairs());
        }


        
    }
}
