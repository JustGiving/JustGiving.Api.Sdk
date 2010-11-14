using System;
using JustGiving.Api.Sdk.Model.Donation;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class DonationApi : ApiClientBase, IDonationApi
    {
        public DonationApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        private string RetrieveLocationFormat(int donationId)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/donation/" + donationId;
        }

        public Donation Retrieve(int donationId)
        {
            var locationFormat = RetrieveLocationFormat(donationId);
            return Parent.HttpChannel.PerformApiRequest<Donation>("GET", locationFormat);
        }

        public void RetrieveAsync(int donationId, Action<Donation> callback)
        {
            var locationFormat = RetrieveLocationFormat(donationId);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        private string RetrieveStatusLocationFormat(int donationId)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/donation/" + donationId + "/status";
        }

        public DonationStatus RetrieveStatus(int donationId)
        {
            var locationFormat = RetrieveStatusLocationFormat(donationId);
            return Parent.HttpChannel.PerformApiRequest<DonationStatus>("GET", locationFormat);
        }

        public void RetrieveStatusAsync(int donationId, Action<DonationStatus> callback)
        {
            var locationFormat = RetrieveStatusLocationFormat(donationId);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }
    }
}