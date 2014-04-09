using System.Collections.Generic;
using JustGiving.Api.Data.Sdk.Model.CustomCodes;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    /// <summary>
    /// Provides methods for applying custom codes to fundraising pages and events, either one at a time, or as a batch operation.
    /// </summary>
    public interface ICustomCodesApi
    {
        /// <summary>
        /// Applies a set of custom codes to a single fundrasing page.
        /// </summary>
        /// <param name="pageId">The id of a fundraising page</param>
        /// <param name="codes">The code values</param>
        /// <returns>A standard HTTP response indicating success or failure</returns>
        SetCustomCodesForPageResponse SetPageCustomCodes(int pageId, PageCustomCodes codes);
       
        /// <summary>
        /// Retrieves custom codes of a single fundrasing page.
        /// </summary>
        /// <param name="pageId">The id of a fundraising page</param>
        /// <returns>Custom codes for the page</returns>
        PageCustomCodes GetPageCustomCodes(int pageId);

        /// <summary>
        /// Retrieves custom codes of a single event.
        /// </summary>
        /// <param name="eventId">The id of an event</param>
        /// <returns>Custom codes for the event</returns>
        EventCustomCodes GetEventCustomCodes(int eventId);

        /// <summary>
        /// Applies several sets of custom custom codes to several pages
        /// </summary>
        /// <param name="codes">A collection of custom code values, including the identities of the pages to apply them to</param>
        /// <returns>A collection of HTTP responses, one for each operation in the batch</returns>
        MultiStatus SetPageCustomCodes(IEnumerable<PageCustomCodesListItem> codes);

        /// <summary>
        /// Applies several sets of custom custom codes to several pages using comma-seperated value (CSV) data
        /// </summary>
        /// <param name="csvData">CSV data including the column headers, page ids and the custom codes to apply</param>
        /// <example>
        /// PageId,CustomCode1,CustomCode2,CustomCode3,CustomCode4,CustomCode5,CustomCode6
        /// 1,"£23.99",30,TEAM1,"Hello, world","2.5%"
        /// 6,"£0.90",30,TEAM2,"Testing 1,2,3",,
        /// 7,,,,,
        /// 9,,31,TEAM1,Some value,4.5
        /// </example>
        /// <returns>A collection of HTTP responses, one for each row in the CSV data</returns>
        MultiStatus SetPageCustomCodes(string csvData);

        /// <summary>
        /// Applies custom codes to a single fundraising event
        /// </summary>
        /// <param name="eventId">The id of the fundraising event</param>
        /// <param name="codes">The code values</param>
        /// <returns>A standard HTTP response indicating success or failure</returns>
        SetCustomCodesResponse SetEventCustomCodes(int eventId, EventCustomCodes codes);
        
        /// <summary>
        /// Applies several sets of custom codes to several fundraising events
        /// </summary>
        /// <param name="codes">A collection of custom code values, including the identities of the fundraising events to apply them to</param>
        /// <returns>A collection of HTTP responses, one for each operation in the batch</returns>
        MultiStatus SetEventCustomCodes(IEnumerable<EventCustomCodesListItem> codes);

        /// <summary>
        /// Applies several sets of custom codes to several fundraising events using comma-seperated value (CSV) data
        /// </summary>
        /// <param name="csvData">CSV data including the column headers, fundraising event ids and the custom codes to apply</param>
        /// <returns>A collection of HTTP responses, one for each row in the CSV data</returns>
        MultiStatus SetEventCustomCodes(string csvData);
    }
}