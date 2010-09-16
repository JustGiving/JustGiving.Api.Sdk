using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class AccountApi : ApiClientBase, IAccountApi
    {
        public AccountApi(JustGivingClient parent):base(parent)
        {
        }

        public string Create(CreateAccountRequest request)
        {
            string locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/account";
            var response = Parent.HttpChannel.PerformApiRequest<CreateAccountRequest, AccountRegistrationConfirmation>("PUT", locationFormat, request);
            return response.Email;
        }

        public FundraisingPageSummarys ListAllPages(string email)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/account/" + email + "/pages";
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummarys>("GET", locationFormat);
        }
    }
}