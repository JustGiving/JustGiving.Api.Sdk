using System;
using GG.Api.Services.Data.Sdk.ApiClients;
using JustGiving.Api.Data.Sdk.Model.Pages;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public interface IPagesApi
    {
        PagesCreated Created(DateTime startDate, DateTime endDate, int eventId = 0);
        byte[] Created(DateTime startDate, DateTime endDate, DataFileFormat fileFormat);
        PagesCreated Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate);
        byte[] Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate, DataFileFormat fileFormat);
    }

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

        public PagesCreated Created(DateTime startDate, DateTime endDate, int eventId = 0)
        {
            var uri = BaseRoot + BuildUrl(startDate, endDate, eventId);
            return HttpChannel.PerformRequest<PagesCreated>("GET", uri);
        }
        
        public byte[] Created(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BaseRoot + BuildUrl(startDate, endDate);
            var response = HttpChannel.PerformRawRequest("GET", uri, ContentTypes.GetAcceptContentType(fileFormat));

            return response.Content.Content;
        }


        //public PagesCreated Created(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        //{
        //    var uri = BaseRoot + BuildUrl(startDate, endDate);
        //    return HttpChannel.PerformRequest<PageCreated>("GET", uri, ContentTypes.GetAcceptContentType(fileFormat));
        //}

        public string BuildUrl(DateTime startDate, DateTime endDate, int eventId = 0)
        {
            var uriDatePart = string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", startDate.Date, endDate.Date);
            return eventId == 0 ? "/pages/created" + uriDatePart : "/events/" + eventId + "/pages/created" + uriDatePart;
        }

        //    public PagesCreated GetPagesCreated(DateTime startDate, DateTime endDate)
    //    {
    //        var uri = BuildFormatUri(startDate, endDate);
    //        return Parent.HttpChannel.PerformApiRequest<PagesCreated>("GET", uri);
    //    }

    //    public byte[] GetPagesCreated(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
    //    {
    //        var uri = BuildFormatUri(startDate, endDate);
    //        var request = new HttpRequestMessage { Method = "GET", AcceptContentType = ContentTypes.GetAcceptContentType(fileFormat) };
    //        var response = Parent.HttpChannel.Send(request, uri);

    //        return response.Content.Content;
    //    }

        //public PagesCreated GetPagesCreatedForEvent(int eventId, DateTime startDate, DateTime endDate)
        //{
        //    var uri = BuildFormatUriForEvent(eventId, startDate, endDate);
        //    return Parent.HttpChannel.PerformApiRequest<PagesCreated>("GET", uri);
        //}

    //    public byte[] GetPagesCreatedForEvent(int eventId, DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
    //    {
    //        var uri = BuildFormatUriForEvent(eventId, startDate, endDate);
    //        var request = new HttpRequestMessage { Method = "GET", AcceptContentType = ContentTypes.GetAcceptContentType(fileFormat) };
    //        var response = Parent.HttpChannel.Send(request, uri);

    //        return response.Content.Content;
    //    }

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

    //    private string BuildFormatUri()
    //    {
    //        return FormatUriRoot + "pages/created";
    //    }

    //    private string BuildFormatUri(DateTime startDate, DateTime endDate)
    //    {
    //        return BuildFormatUri() + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", startDate.Date, endDate.Date);
    //    }

        //private string BuildFormatUriForEvent(int eventId)
        //{
        //    return FormatUriRoot + "events/" + eventId + "/pages/created";
        //}

        //private string BuildFormatUriForEvent(int eventId, DateTime startDate, DateTime endDate)
        //{
        //    return BuildFormatUriForEvent(eventId) + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", startDate.Date, endDate.Date);
        //}

        private string BuildFormatUriForSearch(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate)
        {
            return ResourceBase + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}/search?{2}", startDate.Date, endDate.Date, query.GetQueryStringPairs());
        }


        
    }
}
