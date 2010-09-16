using JustGiving.Api.Sdk.Model.Donation;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IDonationApi
    {
        DonationStatus RetrieveStatus(int donationId);
    }
}