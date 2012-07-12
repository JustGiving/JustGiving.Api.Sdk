using System;
using System.Collections.Generic;

namespace JustGiving.Api.Sdk.DataApiClients
{
    public interface IPaymentListClient
    {
        /// <summary>Fetches a list of payments made within a given date range</summary>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <returns>The list of payments</returns>
        IEnumerable<PaymentSummaryListItem> GetPaymentSummaryList(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Downloads a list of payments made within a given date range, in either Microsoft Excel or comma-separated value (CSV) format
        /// </summary>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <param name="fileFormat">Choose between Excel and CSV</param>
        /// <returns>The downloaded data</returns>
        byte[] GetPaymentSummaryList(DateTime startDate, DateTime endDate, DataFileFormat fileFormat);

        /// <summary>
        /// This does nothing.
        /// </summary>
        /// <returns></returns>
        System.Net.HttpStatusCode NoOp();
    }
}