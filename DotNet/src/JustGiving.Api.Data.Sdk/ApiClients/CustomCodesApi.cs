using System.Collections.Generic;
using System.Text;
using JustGiving.Api.Data.Sdk.Model.CustomCodes;
using JustGiving.Api.Sdk;
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

        public PageCustomCodes GetPageCustomCodes(int pageId)
        {
            var uri = ResourceBase + "/" + pageId + "/customcodes" ;
            var response = HttpChannel.PerformRequest<PageCustomCodes>("GET", uri);
            return response;
        }

        public SetCustomCodesResponse SetEventCustomCodes(int eventId, EventCustomCodes codes)
        {
            var uri = BaseRoot + "/events/" + eventId + "/customcodes";

            var response = HttpChannel.PerformRequest<EventCustomCodes, SetCustomCodesResponse>("PUT", uri, codes);

            return response;
        }

        public EventCustomCodes GetEventCustomCodes(int eventId)
        {
            var uri = BaseRoot + "/events/" + eventId + "/customcodes";
            var response = HttpChannel.PerformRequest<EventCustomCodes>("GET", uri);
            return response;
        }

        public SetCustomCodesForPageResponse SetPageCustomCodes(int pageId, PageCustomCodes codes)
        {
            var uri = ResourceBase + "/" + pageId +  "/customcodes";
            var response = HttpChannel.PerformRequest<PageCustomCodes, SetCustomCodesForPageResponse>("PUT", uri, codes);

            return response;
        }


        public MultiStatus SetPageCustomCodes(IEnumerable<PageCustomCodesListItem> codes)
        {
            var uri = ResourceBase + "/customcodes";
            return HttpChannel.PerformRequest<IEnumerable<PageCustomCodesListItem>, MultiStatus>("PUT", uri, codes);
        }

        public MultiStatus SetPageCustomCodes(string csvData)
        {
            return PutCustomCodes("pages", csvData);
        }


        public MultiStatus SetEventCustomCodes(string csvData)
        {
            return PutCustomCodes("events", csvData);
        }


        public MultiStatus SetEventCustomCodes(IEnumerable<EventCustomCodesListItem> codes)
        {
            var uri = BaseRoot + "/events/customcodes";
            var response = HttpChannel.PerformRequest<IEnumerable<EventCustomCodesListItem>, MultiStatus>("PUT", uri, codes);

            return response;
        }

      
        private MultiStatus PutCustomCodes(string resourceType, string csvData)
        {
            // only Xml and Json responses are available: if the client sets something else, we change it
            // to Xml for this request, then change it back at the end
            var tempWireDataFormat = HttpChannel.ClientConfiguration.WireDataFormat;

            if (HttpChannel.ClientConfiguration.WireDataFormat == WireDataFormat.Other)
            {
                HttpChannel.ClientConfiguration.WireDataFormat = WireDataFormat.Xml;
            }

            var uri = BaseRoot + "/" + resourceType + "/customcodes";

            var data = new UTF8Encoding().GetBytes(csvData);
            //var message = new HttpRequestMessage { Method = "PUT", Content = new HttpContent(data, "text/csv"), AcceptContentType = "application/xml" };
            try
            {
                //return HttpChannel.PerformRequest<HttpRequestMessage, MultiStatus>("PUT", uri, message);
                return HttpChannel.PerformRequest<MultiStatus>("PUT", uri, data, "text/csv", "application/xml");
            }
            finally
            {
                HttpChannel.ClientConfiguration.WireDataFormat = tempWireDataFormat;
            }
        }
        
    }
}