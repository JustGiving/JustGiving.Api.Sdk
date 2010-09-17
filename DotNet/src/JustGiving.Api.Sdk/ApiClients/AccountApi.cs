using System;
using JustGiving.Api.Sdk.Http;
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
            if(request == null)
            {
                throw new ArgumentNullException("request", "Request cannot be null.");
            }

            string locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/account";
            var response = Parent.HttpChannel.PerformApiRequest<CreateAccountRequest, AccountRegistrationConfirmation>("PUT", locationFormat, request);
            return response.Email;
        }

        public FundraisingPageSummaries ListAllPages(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email", "Email cannot be null or empty.");
            }

            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/account/" + email + "/pages";
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummaries>("GET", locationFormat);
        }
    }
}