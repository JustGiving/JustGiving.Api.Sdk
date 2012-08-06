using System.Collections.Generic;
using JustGiving.Api.Data.Sdk.Model.CustomCodes;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public interface ICustomCodesApi
    {
        PageCustomCodes RetrievePageCustomCodes(int pageId);
        EventCustomCodes RetrieveEventCustomCodes(int eventId);
        MultiStatus SetPageCustomCodes(IEnumerable<PageCustomCodesListItem> codes);
        MultiStatus SetPageCustomCodes(string csvData);
        SetCustomCodesForPageResponse SetPageCustomCodes(int pageId, PageCustomCodes codes);
        SetCustomCodesResponse SetEventCustomCodes(int eventId, EventCustomCodes codes);
        MultiStatus SetEventCustomCodes(IEnumerable<EventCustomCodesListItem> codes);
        MultiStatus SetEventCustomCodes(string csvData);
    }
}