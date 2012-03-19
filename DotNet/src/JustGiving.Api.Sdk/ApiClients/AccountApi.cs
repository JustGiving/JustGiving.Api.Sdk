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
		public override string ResourceBase
		{
			get { return "{apiKey}/v{apiVersion}/account"; }
		}

        public AccountApi(HttpChannel channel):base(channel)
        {
        }

        public string Create(CreateAccountRequest request)
        {
        	return HttpChannel.Put<CreateAccountRequest, AccountRegistrationConfirmation>(ResourceBase, request).Email;
        }

        public void CreateAsync(CreateAccountRequest request, Action<string> callback)
		{
			HttpChannel.PutAsync<CreateAccountRequest, AccountRegistrationConfirmation>(ResourceBase, request, response => callback(response.Email));
        }

        public string ListAllPagesLocationFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email", "Email cannot be null or empty.");
            }

			return ResourceBase + "/" + email + "/pages";
        }

        public FundraisingPageSummaries ListAllPages(string email)
        {
            var locationFormat = ListAllPagesLocationFormat(email);
			return HttpChannel.Get<FundraisingPageSummaries>(locationFormat);
        }

        public void ListAllPagesAsync(string email, Action<FundraisingPageSummaries> callback)
        {
            var locationFormat = ListAllPagesLocationFormat(email);
			HttpChannel.GetAsync(locationFormat, callback);
        }

        public string IsEmailRegisteredLocationFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email", "Email cannot be null or empty.");
            
            return ResourceBase + "/" + email;
        }

        public bool IsEmailRegistered(string email)
        {
            var locationFormat = IsEmailRegisteredLocationFormat(email);
            var response = HttpChannel.PerformRawRequest("HEAD", locationFormat);
            return ProcessIsEmailRegisteredResponse(response);
        }

        public void IsEmailRegisteredAsync(string email, Action<bool> callback)
        {
            var locationFormat = IsEmailRegisteredLocationFormat(email);
            HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response => callback(ProcessIsEmailRegisteredResponse(response)));
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
			return ResourceBase + "/" + email + "/requestpasswordreminder";
        }

        public void RequestPasswordReminder(string email)
        {
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException("email", "Email cannot be null or empty."); }

            var locationFormat = RequestPasswordReminderLocationFormat(email);
            var response = HttpChannel.PerformRawRequest("GET", locationFormat);
            ProcessRequestPasswordReminder(response);
        }

        public void RequestPasswordReminderAsync(string email)
        {
            var locationFormat = RequestPasswordReminderLocationFormat(email);
            HttpChannel.PerformRawRequestAsync("GET", locationFormat, ProcessRequestPasswordReminder);
        }

        private static void ProcessRequestPasswordReminder(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }
            
            throw ErrorResponseExceptionFactory.CreateException(response, null);
        }
	}
}