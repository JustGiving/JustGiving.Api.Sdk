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

        public void RetrieveAsync(int charityId, Action<Charity> callback)
        {
            var locationFormat = RetrieveLocationFormat(charityId);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public CharityAuthenticationResult Authenticate(AuthenticateCharityUserRequest request)
        {
            var locationFormat = RetrieveAuthenticationLocationFormat();
            return Parent.HttpChannel.PerformApiRequest<AuthenticateCharityUserRequest, CharityAuthenticationResult>("POST", locationFormat, request);
        }
    }
}
