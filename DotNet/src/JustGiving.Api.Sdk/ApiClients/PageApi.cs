using System;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class PageApi : ApiClientBase, IPageApi
    {
        public PageApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        public FundraisingPageSummaries ListAll()
        {
            string locationFormat = GetListAllLocationFormat();
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummaries>("GET", locationFormat);
        }

        public void ListAllAsync(Action<FundraisingPageSummaries> callback)
        {
            string locationFormat = GetListAllLocationFormat();
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public FundraisingPage Retrieve(string pageShortName)
        {
            var locationFormat = GetRetrieveLocationFormat(pageShortName);
            return Parent.HttpChannel.PerformApiRequest<FundraisingPage>("GET", locationFormat);
        }

        public void RetrieveAsync(string pageShortName, Action<FundraisingPage> callback)
        {
            var locationFormat = GetRetrieveLocationFormat(pageShortName);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, Action<FundraisingPageDonations> callback)
        {
            throw new NotImplementedException();
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, int? pageSize, int? pageNumber, Action<FundraisingPageDonations> callback)
        {
            throw new NotImplementedException();
        }

        public void CreateAsync(RegisterPageRequest request, Action<PageRegistrationConfirmation> callback)
        {
            throw new NotImplementedException();
        }

        public void IsPageShortNameRegisteredAsync(string pageShortName, Action<bool> callback)
        {
            throw new NotImplementedException();
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName)
        {
            return RetrieveDonationsForPage(pageShortName, 50, 1);
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName + "/donations";
            locationFormat += "?PageSize=" + pageSize.GetValueOrDefault(50);
            locationFormat += "&PageNum=" + pageNumber.GetValueOrDefault(1);

            return Parent.HttpChannel.PerformApiRequest<FundraisingPageDonations>("GET", locationFormat);
        }

        public PageRegistrationConfirmation Create(RegisterPageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Request cannot be null.");
            }

            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages";
            return Parent.HttpChannel.PerformApiRequest<RegisterPageRequest, PageRegistrationConfirmation>("PUT", locationFormat, request);
        }

        public void UpdateStory(string pageShortName, string storyUpdate)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName;
            Parent.HttpChannel.PerformApiRequest<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate });
        }

        public bool IsPageShortNameRegistered(string pageShortName)
        {
            if(string.IsNullOrEmpty(pageShortName))
            {
                throw new ArgumentNullException("pageShortName", "pageShortName cannot be null.");
            }

            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName;
            var response = Parent.HttpChannel.PerformRawRequest("HEAD", locationFormat);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.NotFound:
                    return false;
                default:
                    throw ErrorResponseExceptionFactory.CreateException(response, response.Content.ReadAsString(), null);
            }
        }

        public void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName + "/images" + "?caption=" + Uri.EscapeDataString(caption);
            var response = Parent.HttpChannel.PerformRawRequest("POST", locationFormat, imageContentType, imageBytes); 
            
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return;
                default:
                    var rawResponse = response.Content.ReadAsString();
                    var potentialErrors = Parent.HttpChannel.TryExtractErrorsFromResponse(rawResponse);
                    throw ErrorResponseExceptionFactory.CreateException(response, rawResponse, potentialErrors);
            }
        }



        private string GetListAllLocationFormat()
        {
            if (string.IsNullOrEmpty(Parent.Configuration.Username) || string.IsNullOrEmpty(Parent.Configuration.Password))
            {
                throw new Exception("Authentication required to list pages.  Please set a valid configuration object.");
            }

            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages";
        }

        private string GetRetrieveLocationFormat(string pageShortName)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName;
        }
    }
}