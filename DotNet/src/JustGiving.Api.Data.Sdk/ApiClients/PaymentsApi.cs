using System;
using System.Collections.Generic;
using System.Net;
using JustGiving.Api.Data.Sdk.Model;
using JustGiving.Api.Data.Sdk.Model.Payment;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public class PaymentsApi : DataApiClientBase, IPaymentsApi
    {
        public PaymentsApi(HttpChannel channel)
            : base(channel)
        {
        }

        public PaymentsApi(HttpChannel channel, DataClientConfiguration dataClientConfiguration)
            : base(channel, dataClientConfiguration)
        {
        }

        public override string ResourceBase
        {
            get { return BaseRoot + "/payments"; }
        }
        
        private string BuildFormatUri(DateTime date1, DateTime date2)
        {
            return ResourceBase + string.Format("/{0:yyyy-MM-dd};{1:yyyy-MM-dd}", date1.Date, date2.Date);
        }

        public IEnumerable<PaymentSummary> RetrievePaymentsBetween(DateTime startDate, DateTime endDate)
        {
            var uri = BuildFormatUri(startDate, endDate);
            return HttpChannel.PerformRequest<IEnumerable<PaymentSummary>>("GET", uri);          
        }

        public byte[] RetrievePaymentsBetween(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
        {
            var uri = BuildFormatUri(startDate, endDate);
            var response = HttpChannel.PerformRawRequest("GET", uri, ContentTypes.GetAcceptContentType(fileFormat));

            return response.Content.Content;
        }

        public HttpStatusCode NoOp()
        {
            var uri = BaseRoot + "/noop";
            var response = HttpChannel.PerformRawRequest("HEAD", uri);
            return response.StatusCode;
        }

        public byte[] RetrieveReport(int paymentId, DataFileFormat fileFormat)
        {
            var uri = Uri.Combine(ResourceBase, paymentId.ToString());
            var response = HttpChannel.PerformRawRequest("GET", uri, ContentTypes.GetAcceptContentType(fileFormat));
            return response.Content.Content;
        }

        public T RetrieveReport<T>(int paymentId)
        {
            var uri = Uri.Combine(ResourceBase, paymentId.ToString());
            return HttpChannel.PerformRequest<T>("GET", uri);
        }
    }

    public class Uri
    {
        public static string Combine(string firstUriPart, string secondUriPart)
        {
            return string.Format("{0}/{1}", firstUriPart.TrimEnd('/'), secondUriPart.TrimStart('/'));
        }
    }
}