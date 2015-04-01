using System;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Donation;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class DonationApi : ApiClientBase, IDonationApi
	{
		public override string ResourceBase
		{
			get { return "{apiKey}/v{apiVersion}/donation"; }
		}

        public DonationApi(HttpChannel channel)
            : base(channel)
        {
        }

        private string RetrieveLocationFormat(int donationId)
        {
			return ResourceBase + "/" + donationId;
        }

        public Donation Retrieve(int donationId)
        {
            var locationFormat = RetrieveLocationFormat(donationId);
            return HttpChannel.PerformRequest<Donation>("GET", locationFormat);
        }

        public void RetrieveAsync(int donationId, Action<Donation> callback)
        {
            var locationFormat = RetrieveLocationFormat(donationId);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        private string RetrieveStatusLocationFormat(int donationId)
        {
			return RetrieveLocationFormat(donationId) + "/status";
        }

        public DonationStatus RetrieveStatus(int donationId)
        {
            var locationFormat = RetrieveStatusLocationFormat(donationId);
            return HttpChannel.PerformRequest<DonationStatus>("GET", locationFormat);
        }

        public void RetrieveStatusAsync(int donationId, Action<DonationStatus> callback)
        {
            var locationFormat = RetrieveStatusLocationFormat(donationId);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        private string RetrieveResourceEndpoint(string reference)
        {
            return ResourceBase + "/ref/" + reference;
        }

        public ReferencedDonation Retrieve(string reference)
        {
            var resourceEndpoint = RetrieveResourceEndpoint(reference);
            var result = HttpChannel.PerformRequest<ReferencedDonation>("GET", resourceEndpoint);
            return result;
        }

        [DataContract(Name = "donationsByReference", Namespace = "")]
        public class ReferencedDonation : Donation
        {
        }
    }
}