using System;
using System.Collections.Generic;
using JustGiving.Api.Data.Sdk.Model;
using JustGiving.Api.Data.Sdk.Model.Payment;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public interface IPaymentsApi
    {
        IEnumerable<PaymentSummary> RetrievePaymentsBetween(DateTime startDate, DateTime endDate);
        byte[] RetrievePaymentsBetween(DateTime startDate, DateTime endDate, DataFileFormat fileFormat);
        System.Net.HttpStatusCode NoOp();
        byte[] RetrieveReport(int paymentId, DataFileFormat fileFormat);
        T RetrieveReport<T>(int paymentId);
    }
}