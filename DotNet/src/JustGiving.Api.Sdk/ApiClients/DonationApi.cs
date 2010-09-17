using JustGiving.Api.Sdk.Model.Donation;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class DonationApi : ApiClientBase, IDonationApi
    {
        public DonationApi(JustGivingClient parent)
            : base(parent)
        {
        }

        public DonationStatus RetrieveStatus(int donationId)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/donation/" + donationId + "/status";
            return Parent.HttpChannel.PerformApiRequest<DonationStatus>("GET", locationFormat);
        }
    }
}