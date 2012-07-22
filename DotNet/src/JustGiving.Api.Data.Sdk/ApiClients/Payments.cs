﻿using System;
using System.Collections.Generic;
using System.Net;
using JustGiving.Api.Data.Sdk.Model.Payment;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    /// <summary>
    /// Makes Http calls to the Payment List resources made available by the JustGiving Data Api
    /// </summary>
    public class Payments : DataApiClientBase, IPayments
    {
        public Payments(HttpChannel channel)
            : base(channel)
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

        public IEnumerable<PaymentSummary> PaymentsBetween(DateTime startDate, DateTime endDate)
        {
            var uri = BuildFormatUri(startDate, endDate);
            return HttpChannel.PerformRequest<IEnumerable<PaymentSummary>>("GET", uri);          
        }

        public byte[] GetPaymentSummaryList(DateTime startDate, DateTime endDate, DataFileFormat fileFormat)
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

    }
}