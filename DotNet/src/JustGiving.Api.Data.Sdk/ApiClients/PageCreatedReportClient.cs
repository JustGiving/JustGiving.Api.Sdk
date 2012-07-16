using System;
using System.Web;
using GG.Api.Sdk.Http;
using GG.Api.Sdk.Http.DataPackets;
using GG.Api.Services.Data.Dto.PagesCreated;

namespace GG.Api.Services.Data.Sdk.ApiClients
{
    public class PageCreatedReportClient : DataApiClientBase, IPageCreatedReportClient
    {
        public PageCreatedReportClient(JustGivingClientBase parent) : base(parent)
        {
        }

        public PagesCreated GetPagesCreated(DateTime startDate, DateTime endDate)
        {
            var uri = BuildFormatUri(startDate, endDate);
            return Parent.HttpChannel.PerformApiRequest<PagesCreated>("GET", uri);
        }

        public byte[] GetPagesCreated(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BuildFormatUri(startDate, endDate);
            var request = new HttpRequestMessage { Method = "GET", AcceptContentType = ContentTypes.GetAcceptContentType(fileFormat) };
            var response = Parent.HttpChannel.Send(request, uri);

            return response.Content.Content;
        }

        public PagesCreated GetPagesCreatedForEvent(int eventId, DateTime startDate, DateTime endDate)
        {
            var uri = BuildFormatUriForEvent(eventId, startDate, endDate);
            return Parent.HttpChannel.PerformApiRequest<PagesCreated>("GET", uri);
        }

        public byte[] GetPagesCreatedForEvent(int eventId, DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BuildFormatUriForEvent(eventId, startDate, endDate);
            var request = new HttpRequestMessage { Method = "GET", AcceptContentType = ContentTypes.GetAcceptContentType(fileFormat) };
            var response = Parent.HttpChannel.Send(request, uri);

            return response.Content.Content;
        }

        public PagesCreated Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate)
        {
            var uri = BuildFormatUriForSearch(query, startDate, endDate);
            return Parent.HttpChannel.PerformApiRequest<PagesCreated>("GET", uri);
        }

        public byte[] Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BuildFormatUriForSearch(query, startDate, endDate);
            var request = new HttpRequestMessage { Method = "GET", AcceptContentType = ContentTypes.GetAcceptContentType(fileFormat) };
            var response = Parent.HttpChannel.Send(request, uri);

            return response.Content.Content;
        }

        private string BuildFormatUri()
        {
            return FormatUriRoot + "pages/created";
        }

        private string BuildFormatUri(DateTime startDate, DateTime endDate)
        {
            return BuildFormatUri() + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", startDate.Date, endDate.Date);
        }

        private string BuildFormatUriForEvent(int eventId)
        {
            return FormatUriRoot + "events/" + eventId + "/pages/created";
        }

        private string BuildFormatUriForEvent(int eventId, DateTime startDate, DateTime endDate)
        {
            return BuildFormatUriForEvent(eventId) + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", startDate.Date, endDate.Date);
        }

        private string BuildFormatUriForSearch(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate)
        {
            return BuildFormatUri() + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}/search?{2}", startDate.Date, endDate.Date, query.GetQueryStringPairs());
        }
    }
}
