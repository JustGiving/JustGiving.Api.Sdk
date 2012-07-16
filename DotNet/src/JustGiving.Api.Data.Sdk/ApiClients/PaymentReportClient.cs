using GG.Api.Sdk.Http;
using GG.Api.Services.Data.Dto.Payment.Donations;
using HttpRequestMessage = GG.Api.Sdk.Http.DataPackets.HttpRequestMessage;

namespace GG.Api.Services.Data.Sdk.ApiClients
{
    /// <summary>
    /// Makes Http calls to the Payment Report resources made available by the JustGiving Data Api
    /// </summary>
    public class PaymentReportClient: DataApiClientBase, IPaymentReportClient
    {
        public PaymentReportClient(JustGivingClientBase parent)
            : base(parent)
        {
        }

        private string BuildFormatUri(int paymentId)
        {
            return FormatUriRoot + "payments/" + paymentId;
        }

        public Payment GetDonationPaymentReport(int paymentId)
        {
            var uri = BuildFormatUri(paymentId);
            return Parent.HttpChannel.PerformApiRequest<Payment>("GET", uri);
        }

        public Dto.Payment.GiftAid.Payment GetGiftAidPaymentReport(int paymentId)
        {
            var uri = BuildFormatUri(paymentId);
            return Parent.HttpChannel.PerformApiRequest<Dto.Payment.GiftAid.Payment>("GET", uri);
        }

        public byte[] GetPaymentReport(int paymentId, DataFileFormat fileFormat)
        {
            var uri = BuildFormatUri(paymentId);
            var request = new HttpRequestMessage { Method = "GET", AcceptContentType = ContentTypes.GetAcceptContentType(fileFormat) };
            var response = Parent.HttpChannel.Send(request, uri);

            return response.Content.Content;
        }

    }
}