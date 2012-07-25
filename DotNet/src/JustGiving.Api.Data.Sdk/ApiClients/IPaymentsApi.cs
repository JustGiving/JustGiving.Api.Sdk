using System;
using System.Collections.Generic;
using JustGiving.Api.Data.Sdk.Model.Payment;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    /// <summary>
    /// Provides methods for accessing payment lists.
    /// </summary>
    public interface IPaymentsApi
    {
        /// <summary>Fetches a list of payments made within a given date range</summary>
        /// <param name="startDate">The lower bound of a date range (inclusive)</param>
        /// <param name="endDate">The upper bound of a date range (inclusive)</param>
        /// <returns>The list of payments</returns>
        IEnumerable<PaymentSummary> PaymentsBetween(DateTime startDate, DateTime endDate);

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

        /// <summary>
        /// Fetches a detailed donation report for a single payment in either Microsoft Excel or comma-separated value (CSV) format
        /// </summary>
        /// <param name="paymentId">The payment id</param>
        /// <param name="fileFormat">Choose between Excel and CSV</param>
        /// <returns>The downloaded report</returns>
        byte[] ReportFor(int paymentId, DataFileFormat fileFormat);
    }
}