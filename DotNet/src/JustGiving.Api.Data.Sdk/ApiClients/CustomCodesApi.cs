using System.Collections.Generic;
using JustGiving.Api.Data.Sdk.Model.CustomCodes;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    /// <summary>
    /// Makes Http calls to the Custom Code resources of the JustGiving Data Api.
    /// </summary>
    public class CustomCodesApi : DataApiClientBase, ICustomCodesApi
    {
        public CustomCodesApi(HttpChannel channel)
            : base(channel)
        {
        }

        public override string ResourceBase
        {
            get { return BaseRoot + "/pages"; }
        }



        //private string BuildFormatUri(string resourceType, int resourceId)
        //{
        //    return FormatUriRoot + resourceType + "/" + resourceId + "/customcodes";
        //}

        //public PageCustomCodes GetPageCustomCodes(int pageId)
        //{
        //    var uri = BuildFormatUri("pages", pageId);
        //    var response = Parent.HttpChannel.PerformApiRequest<PageCustomCodes>("GET", uri);
        //    return response;
        //}

        //public EventCustomCodes GetEventCustomCodes(int eventId)
        //{
        //    var uri = BuildFormatUri("events", eventId);
        //    var response = Parent.HttpChannel.PerformApiRequest<EventCustomCodes>("GET", uri);
        //    return response;
        //}

        public SetCustomCodesResponse SetPageCustomCodes(int pageId, PageCustomCodes codes)
        {
            var uri = ResourceBase + "/" + pageId +  "/customcodes";
            var response = HttpChannel.PerformRequest<PageCustomCodes, SetCustomCodesResponse>("PUT", uri, codes);

            return response;
        }

        
        public PageCustomCodes GetPageCustomCodes(int pageId)
        {
            throw new System.NotImplementedException();
        }

        public EventCustomCodes GetEventCustomCodes(int eventId)
        {
            throw new System.NotImplementedException();
        }

        public MultiStatus SetPageCustomCodes(IEnumerable<PageCustomCodesListItem> codes)
        {
            return HttpChannel.PerformRequest<IEnumerable<PageCustomCodesListItem>, MultiStatus>("PUT", ResourceBase, codes);
        }

        public MultiStatus SetPageCustomCodes(string csvData)
        {
            throw new System.NotImplementedException();
        }

        public HttpResponseMessage SetEventCustomCodes(int eventId, EventCustomCodes codes)
        {
            throw new System.NotImplementedException();
        }

        public MultiStatus SetEventCustomCodes(IEnumerable<EventCustomCodesListItem> codes)
        {
            throw new System.NotImplementedException();
        }

        public MultiStatus SetEventCustomCodes(string csvData)
        {
            throw new System.NotImplementedException();
        }

//        public MultiStatus SetPageCustomCodes(string csvData)
//        {
//            return PutCustomCodes("pages", csvData);
//        }

        //public HttpResponseMessage SetEventCustomCodes(int eventId, EventCustomCodes codes)
        //{
        //    var uri = BuildFormatUri("events", eventId);

        //    var response = Parent.HttpChannel.PerformApiRequest("PUT", uri, codes);

        //    return response;
        //}

        //public MultiStatus SetEventCustomCodes(IEnumerable<EventCustomCodesListItem> codes)
        //{
        //    var uri = FormatUriRoot + "events/customcodes";

        //    var response = Parent.HttpChannel.PerformApiRequest<IEnumerable<EventCustomCodesListItem>, MultiStatus>("PUT", uri, codes);

        //    return response;
        //}

        //public MultiStatus SetEventCustomCodes(string csvData)
        //{
        //    return PutCustomCodes("events", csvData);
        //}

        //private MultiStatus PutCustomCodes(string resourceType, string csvData)
        //{
        //    // only Xml and Json responses are available: if the client sets something else, we change it
        //    // to Xml for this request, then change it back at the end

        //    var tempWireDataFormat = Parent.Configuration.WireDataFormat;
        //    if (Parent.Configuration.WireDataFormat == WireDataFormat.Other)
        //    {
        //        Parent.Configuration.WireDataFormat = WireDataFormat.Xml;
        //    }

        //    var uri = FormatUriRoot + resourceType + "/customcodes";

        //    var data = new UTF8Encoding().GetBytes(csvData);
        //    var message = new HttpRequestMessage { Method = "PUT", Content = new HttpContent(data, "text/csv"), AcceptContentType = "application/xml" };
        //    try
        //    {
        //        return Parent.HttpChannel.Send<MultiStatus>(message, uri);
        //    }
        //    finally
        //    {
        //        Parent.Configuration.WireDataFormat = tempWireDataFormat;
        //    }
        //}
        
    }
}