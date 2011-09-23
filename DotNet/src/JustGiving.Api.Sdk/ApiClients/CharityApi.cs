using System;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CharityApi: ApiClientBase, ICharityApi
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
    }
}
