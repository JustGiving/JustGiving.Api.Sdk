using System.Collections.Generic;
using System.Text;
using GG.Api.Sdk;
using GG.Api.Sdk.Http;
using GG.Api.Sdk.Http.DataPackets;
using GG.Api.Services.Data.Dto;
using GG.Api.Services.Data.Dto.CustomCodes;

namespace GG.Api.Services.Data.Sdk.ApiClients
{
    /// <summary>
    /// Makes Http calls to the Custom Code resources of the JustGiving Data Api.
    /// </summary>
    public class CustomCodesClient : DataApiClientBase, ICustomCodesClient
    {
        public CustomCodesClient(JustGivingClientBase parent) : base(parent)
        {
        }

        private string BuildFormatUri(string resourceType, int resourceId)
        {
            return FormatUriRoot + resourceType + "/" + resourceId + "/customcodes";
        }

        public PageCustomCodes GetPageCustomCodes(int pageId)
        {
            var uri = BuildFormatUri("pages", pageId);
            var response = Parent.HttpChannel.PerformApiRequest<PageCustomCodes>("GET", uri);
            return response;
        }

        public EventCustomCodes GetEventCustomCodes(int eventId)
        {
            var uri = BuildFormatUri("events", eventId);
            var response = Parent.HttpChannel.PerformApiRequest<EventCustomCodes>("GET", uri);
            return response;
        }

        public HttpResponseMessage SetPageCustomCodes(int pageId, PageCustomCodes codes)
        {
            var uri = BuildFormatUri("pages", pageId);

            var response = Parent.HttpChannel.PerformApiRequest("PUT", uri, codes);

            return response;
        }

        public MultiStatus SetPageCustomCodes(IEnumerable<PageCustomCodesListItem> codes)
        {
            var uri = FormatUriRoot + "pages/customcodes";

            var response = Parent.HttpChannel.PerformApiRequest<IEnumerable<PageCustomCodesListItem>, MultiStatus>("PUT", uri, codes);

            return response;
        }

        public MultiStatus SetPageCustomCodes(string csvData)
        {
            return PutCustomCodes("pages", csvData);
        }

        public HttpResponseMessage SetEventCustomCodes(int eventId, EventCustomCodes codes)
        {
            var uri = BuildFormatUri("events", eventId);

            var response = Parent.HttpChannel.PerformApiRequest("PUT", uri, codes);

            return response;
        }

        public MultiStatus SetEventCustomCodes(IEnumerable<EventCustomCodesListItem> codes)
        {
            var uri = FormatUriRoot + "events/customcodes";

            var response = Parent.HttpChannel.PerformApiRequest<IEnumerable<EventCustomCodesListItem>, MultiStatus>("PUT", uri, codes);

            return response;
        }

        public MultiStatus SetEventCustomCodes(string csvData)
        {
            return PutCustomCodes("events", csvData);
        }

        private MultiStatus PutCustomCodes(string resourceType, string csvData)
        {
            // only Xml and Json responses are available: if the client sets something else, we change it
            // to Xml for this request, then change it back at the end

            var tempWireDataFormat = Parent.Configuration.WireDataFormat;
            if (Parent.Configuration.WireDataFormat == WireDataFormat.Other)
            {
                Parent.Configuration.WireDataFormat = WireDataFormat.Xml;
            }

            var uri = FormatUriRoot + resourceType + "/customcodes";

            var data = new UTF8Encoding().GetBytes(csvData);
            var message = new HttpRequestMessage { Method = "PUT", Content = new HttpContent(data, "text/csv"), AcceptContentType = "application/xml" };
            try
            {
                return Parent.HttpChannel.Send<MultiStatus>(message, uri);
            }
            finally
            {
                Parent.Configuration.WireDataFormat = tempWireDataFormat;
            }
        }
    }
}