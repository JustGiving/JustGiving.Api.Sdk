using System;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class AccountApi : ApiClientBase, IAccountApi
    {
        public AccountApi(JustGivingClientBase parent):base(parent)
        {
        }

        public string CreateLocationFormat(CreateAccountRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException("request", "Request cannot be null.");
            }

            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/account";
        }

        public string Create(CreateAccountRequest request)
        {
            var locationFormat = CreateLocationFormat(request);
            return Parent.HttpChannel.PerformApiRequest<CreateAccountRequest, AccountRegistrationConfirmation>("PUT", locationFormat, request).Email;
        }

        public void CreateAsync(CreateAccountRequest request, Action<string> callback)
        {
            string locationFormat = CreateLocationFormat(request);
            Parent.HttpChannel.PerformApiRequestAsync<CreateAccountRequest, AccountRegistrationConfirmation>("PUT", locationFormat, request, response=>CreateAsyncEnd(response, callback));
        }

        private static void CreateAsyncEnd(AccountRegistrationConfirmation response, Action<string> clientCallback)
        {
            clientCallback(response.Email);
        }

        public string ListAllPagesLocationFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email", "Email cannot be null or empty.");
            }

            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/account/" + email + "/pages";
        }

        public FundraisingPageSummaries ListAllPages(string email)
        {
            var locationFormat = ListAllPagesLocationFormat(email);
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummaries>("GET", locationFormat);
        }

        public void ListAllPagesAsync(string email, Action<FundraisingPageSummaries> callback)
        {
            var locationFormat = ListAllPagesLocationFormat(email);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public string IsEmailRegisteredLocationFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email", "Email cannot be null or empty.");
            
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/account/" + email;
        }

        public bool IsEmailRegistered(string email)
        {
            var locationFormat = IsEmailRegisteredLocationFormat(email);
            var response = Parent.HttpChannel.PerformRawRequest("HEAD", locationFormat);
            return ProcessIsEmailRegisteredResponse(response);
        }

        public void IsEmailRegisteredAsync(string email, Action<bool> callback)
        {
            var locationFormat = IsEmailRegisteredLocationFormat(email);
            Parent.HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response => IsEmailRegisteredAsyncEnd(response, callback));
        }

        private static void IsEmailRegisteredAsyncEnd(HttpResponseMessage response, Action<bool> clientCallback)
        {
            var isEmailRegistered = ProcessIsEmailRegisteredResponse(response);
            clientCallback(isEmailRegistered);
        }

        private static bool ProcessIsEmailRegisteredResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.NotFound:
                    return false;
                default:
                    throw ErrorResponseExceptionFactory.CreateException(response, null);
            }
        }

        public string RequestPasswordReminderLocationFormat(string email)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/account/" + email + "/requestpasswordreminder";
        }

        public void RequestPasswordReminder(string email)
        {
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException("email", "Email cannot be null or empty."); }

            var locationFormat = RequestPasswordReminderLocationFormat(email);
            var response = Parent.HttpChannel.PerformRawRequest("GET", locationFormat);
            ProcessRequestPasswordReminder(response);
        }

        public void RequestPasswordReminderAsync(string email)
        {
            var locationFormat = RequestPasswordReminderLocationFormat(email);
            Parent.HttpChannel.PerformRawRequestAsync("GET", locationFormat, ProcessRequestPasswordReminder);
        }

        private static void ProcessRequestPasswordReminder(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return;
                default:
                    throw ErrorResponseExceptionFactory.CreateException(response, null);
            }
        }
    }
}