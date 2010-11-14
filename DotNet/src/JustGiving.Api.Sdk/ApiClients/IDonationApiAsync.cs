using System;
using JustGiving.Api.Sdk.Model.Donation;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IDonationApiAsync
    {
        void RetrieveAsync(int donationId, Action<Donation> callback);
        void RetrieveStatusAsync(int donationId, Action<DonationStatus> callback);
    }
}