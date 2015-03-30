using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CharityApi : ApiClientBase, ICharityApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/charity"; }
        }

        public CharityApi(HttpChannel channel)
            : base(channel)
        {
        }

        public string RetrieveLocationFormat(int charityId)
        {
            return ResourceBase + "/" + charityId;
        }

        public string RetrieveAuthenticationLocationFormat()
        {
            return ResourceBase + "/authenticate";
        }

        public Charity Retrieve(int charityId)
        {
            var locationFormat = RetrieveLocationFormat(charityId);
            return HttpChannel.PerformRequest<Charity>("GET", locationFormat);
        }

        public CharityEvents RetrieveEvents(int charityId)
        {
            return RetrieveEvents(charityId, 1, 50);
        }

        public CharityEvents RetrieveEvents(int charityId, int pageNumber, int pageSize)
        {
            var locationFormat = RetrieveLocationFormat(charityId) + "/events" + string.Format("?pagenum={0}&pagesize={1}", pageNumber, pageSize);
            return HttpChannel.PerformRequest<CharityEvents>("GET", locationFormat);
        }

        public void RetrieveAsync(int charityId, Action<Charity> callback)
        {
            var locationFormat = RetrieveLocationFormat(charityId);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public void RetrieveEventsAsync(int charityId, Action<CharityEvents> callback)
        {
            RetrieveEventsAsync(charityId, 1, 50, callback);
        }

        public void RetrieveEventsAsync(int charityId, int pageNumber, int pageSize, Action<CharityEvents> callback)
        {
            var locationFormat = RetrieveLocationFormat(charityId) + "/events" + string.Format("?pagenum={0}&pagesize={1}", pageNumber, pageSize);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public CharityAuthenticationResult Authenticate(AuthenticateCharityUserRequest request)
        {
            var locationFormat = RetrieveAuthenticationLocationFormat();
            return HttpChannel.PerformRequest<AuthenticateCharityUserRequest, CharityAuthenticationResult>("POST", locationFormat, request);
        }

        public void AuthenticateAsync(AuthenticateCharityUserRequest request, Action<CharityAuthenticationResult> callback)
        {
            var locationFormat = RetrieveAuthenticationLocationFormat();
            HttpChannel.PerformRequestAsync("POST", locationFormat, callback);
        }

        public string CharityDonationsResourceEndpoint(int charityId)
        {
            return ResourceBase + "/" + charityId + "/donations";
        }

        public CharityDonationsResult CharityDonations(int charityId)
        {
            var resourceEndpoint = CharityDonationsResourceEndpoint(charityId);
            var result = HttpChannel.PerformRequest<CharityDonationsResult>("GET", resourceEndpoint);
            return result;
        }

        [DataContract(Name = "charityDonations", Namespace = "")]
        public class CharityDonationsResult
        {
            [DataMember(Name = "donations")]
            public DonationsCollection Donations { get; set; }

        }

        [CollectionDataContract(Name = "donations", ItemName = "donation", Namespace = "")]
        public class DonationsCollection : List<CharityDonationResult>
        {

        }

        [DataContract(Name = "donation", Namespace = "")]
        public class CharityDonationResult
        {
            [DataMember(Name = "donorDisplayName")]
            public string DonorDisplayName { get; set; }

            [DataMember(Name = "message")]
            public string Message { get; set; }

            [DataMember(Name = "amount")]
            public decimal? Amount { get; set; }

            [DataMember(Name = "estimatedTaxReclaim")]
            public decimal? EstimatedTaxReclaim { get; set; }

            [DataMember(Name = "imageUrl")]
            public string ImageUrl { get; set; }

            [DataMember(Name = "donationDate")]
            public DateTime DonationDate { get; set; }

            [DataMember(Name = "currencyCode")]
            public string CurrencyCode { get; set; }

            [DataMember(Name = "donorLocalAmount")]
            public decimal? DonorLocalAmount { get; set; }

            [DataMember(Name = "donorLocalCurrencyCode")]
            public string DonorLocalCurrencyCode { get; set; }
        }
    }
}
