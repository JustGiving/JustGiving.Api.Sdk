namespace GG.Api.Services.Data.Sdk.ApiClients
{
    /// <summary>
    /// Provides methods for accessing payment reports.
    /// </summary>
    public interface IPaymentReportClient
    {
        /// <summary>
        /// Fetches a detailed donation report for a single payment.
        /// </summary>
        /// <param name="paymentId">The payment id</param>
        /// <returns>The report</returns>
        Dto.Payment.Donations.Payment GetDonationPaymentReport(int paymentId);

        /// <summary>
        /// Fetches a detailed donation report for a single payment in either Microsoft Excel or comma-separated value (CSV) format
        /// </summary>
        /// <param name="paymentId">The payment id</param>
        /// <param name="fileFormat">Choose between Excel and CSV</param>
        /// <returns>The downloaded report</returns>
        byte[] GetPaymentReport(int paymentId, DataFileFormat fileFormat);

        /// <summary>
        /// Fetches a detailed GiftAid report for a single payment.
        /// </summary>
        /// <param name="paymentId">The payment id</param>
        /// <returns>The report</returns>
        Dto.Payment.GiftAid.Payment GetGiftAidPaymentReport(int paymentId);
    }
}
