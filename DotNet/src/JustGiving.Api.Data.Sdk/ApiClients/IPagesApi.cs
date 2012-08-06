using System;
using JustGiving.Api.Data.Sdk.Model;
using JustGiving.Api.Data.Sdk.Model.Pages;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public interface IPagesApi
    {
        PagesCreated RetrievePagesCreated(DateTime startDate, DateTime endDate, int eventId = 0);
        byte[] RetrievePagesCreated(DateTime startDate, DateTime endDate, DataFileFormat fileFormat);
        PagesCreated Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate);
        byte[] Search(PageCreatedSearchQuery query, DateTime startDate, DateTime endDate, DataFileFormat fileFormat);
    }
}