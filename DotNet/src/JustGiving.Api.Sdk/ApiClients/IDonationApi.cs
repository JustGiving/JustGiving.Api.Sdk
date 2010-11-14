using JustGiving.Api.Sdk.Model.Donation;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IDonationApi: IDonationApiAsync
    {
        Donation Retrieve(int donationId);
        DonationStatus RetrieveStatus(int donationId);
    }
}