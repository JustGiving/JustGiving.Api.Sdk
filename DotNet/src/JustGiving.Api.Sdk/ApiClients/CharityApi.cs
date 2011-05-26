using System;
using JustGiving.Api.Sdk.Model;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CharityApi: ApiClientBase, ICharityApi
    {
        public CharityApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        public string RetrieveLocationFormat(int charityId)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/charity/" + charityId;
        }

        public string RetrieveAuthenticationLocationFormat()
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/charity/authenticate";
        }

        public Charity Retrieve(int charityId)
        {
            var locationFormat = RetrieveLocationFormat(charityId);
            return Parent.HttpChannel.PerformApiRequest<Charity>("GET", locationFormat);
        }

        public CharityEvents RetrieveEvents(int charityId)
        {
            return RetrieveEvents(charityId, 1, 50);
        }

        public CharityEvents RetrieveEvents(int charityId, int pageNumber, int pageSize)
        {
            var locationFormat = RetrieveLocationFormat(charityId) + "/events" + string.Format("?pagenum={0}&pagesize={1}", pageNumber, pageSize);
            return Parent.HttpChannel.PerformApiRequest<CharityEvents>("GET", locationFormat);
        }

        public void RetrieveAsync(int charityId, Action<Charity> callback)
        {
            var locationFormat = RetrieveLocationFormat(charityId);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public void RetrieveEventsAsync(int charityId, Action<CharityEvents> callback)
        {
            RetrieveEventsAsync(charityId, 1, 50, callback);
        }

        public void RetrieveEventsAsync(int charityId, int pageNumber, int pageSize, Action<CharityEvents> callback)
        {
            var locationFormat = RetrieveLocationFormat(charityId) + "/events" + string.Format("?pagenum={0}&pagesize={1}", pageNumber, pageSize);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public CharityAuthenticationResult Authenticate(AuthenticateCharityUserRequest request)
        {
            var locationFormat = RetrieveAuthenticationLocationFormat();
            return Parent.HttpChannel.PerformApiRequest<AuthenticateCharityUserRequest, CharityAuthenticationResult>("POST", locationFormat, request);
        }
    }
}
