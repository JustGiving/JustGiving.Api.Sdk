using System;

namespace GG.Api.Services.Data.Sdk.ApiClients
{
    /// <summary>
    /// Provides methods for accessing the Page Created report.
    /// </summary>
    public interface IPageCreatedReportClient
    {
        /// <summary>
        /// Gets a list of all pages created within a given date range
        /// </summary>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <returns>A list of pages created</returns>
        Dto.PagesCreated.PagesCreated GetPagesCreated(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets a list of all pages created within a given date range
        /// </summary>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <param name="fileFormat">Choose between Excel and CSV</param>
        /// <returns>The downloaded data</returns>
        byte[] GetPagesCreated(DateTime startDate, DateTime endDate, DataFileFormat fileFormat);

        /// <summary>
        /// Gets a list of all pages created for a specific event within a given date range
        /// </summary>
        /// <param name="eventId">The Id of a fundraising event</param>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <returns>A list of pages created</returns>
        Dto.PagesCreated.PagesCreated GetPagesCreatedForEvent(int eventId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets a list of all pages created for a specific event within a given date range
        /// </summary>
        /// <param name="eventId">The Id of a fundraising event</param>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <param name="fileFormat">Choose between Excel and CSV</param>
        /// <returns>The downloaded data</returns>
        byte[] GetPagesCreatedForEvent(int eventId, DateTime startDate, DateTime endDate, DataFileFormat fileFormat);

        /// <summary>
        /// Search for pages created within a given date range based on custom codes and other criteria.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <returns></returns>
        Dto.PagesCreated.PagesCreated Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Search for pages created within a given date range based on custom codes and other criteria.
        /// </summary>
        /// <param name="fileFormat">Choose between Excel and CSV</param>
        /// <param name="query"></param>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <returns></returns>
        byte[] Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate, DataFileFormat fileFormat);
    }
}
