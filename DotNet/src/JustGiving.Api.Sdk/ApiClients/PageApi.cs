using System;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
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
            var locationFormat = GetListAllLocationFormat();
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummaries>("GET", locationFormat);
        }

        public void ListAllAsync(Action<FundraisingPageSummaries> callback)
        {
            var locationFormat = GetListAllLocationFormat();
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

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName)
        {
            return RetrieveDonationsForPage(pageShortName, 50, 1);
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = GetRetrieveDonationsForPageLocationFormat(pageShortName, pageSize, pageNumber);
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageDonations>("GET", locationFormat);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, Action<FundraisingPageDonations> callback)
        {
            RetrieveDonationsForPageAsync(pageShortName, 50, 1, callback);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, int? pageSize, int? pageNumber, Action<FundraisingPageDonations> callback)
        {
            var locationFormat = GetRetrieveDonationsForPageLocationFormat(pageShortName, pageSize, pageNumber);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public PageRegistrationConfirmation Create(RegisterPageRequest request)
        {
            string locationFormat = GetCreateLocationFormat(request);
            return Parent.HttpChannel.PerformApiRequest<RegisterPageRequest, PageRegistrationConfirmation>("PUT", locationFormat, request);
        }

        public void CreateAsync(RegisterPageRequest request, Action<PageRegistrationConfirmation> callback)
        {
            var locationFormat = GetCreateLocationFormat(request);
            Parent.HttpChannel.PerformApiRequestAsync("PUT", locationFormat, request, callback);
        }

        public void UpdateStory(string pageShortName, string storyUpdate)
        {
            var locationFormat = GetUpdateStoryLocationFormat(pageShortName);
            Parent.HttpChannel.PerformApiRequest<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate });
        }

        public void UpdateStoryAsync(string pageShortName, string storyUpdate)
        {
            var locationFormat = GetUpdateStoryLocationFormat(pageShortName);
            Parent.HttpChannel.PerformApiRequestAsync<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate }, response=>{});
        }

        public bool IsPageShortNameRegistered(string pageShortName)
        {
            var locationFormat = GetIsPageShortNameRegisteredLocationFormat(pageShortName);
            var response = Parent.HttpChannel.PerformRawRequest("HEAD", locationFormat);
            return ProcessIsPageShortNameRegisteredResponse(response);
        }

        public void IsPageShortNameRegisteredAsync(string pageShortName, Action<bool> callback)
        {
            var locationFormat = GetIsPageShortNameRegisteredLocationFormat(pageShortName);
            Parent.HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response=>IsPageShortNameRegisteredAsyncEnd(response, callback));
        }

        public void UploadImageAsync(string pageShortName, string caption, byte[] imageBytes, string imageContentType)
        {
            throw new NotImplementedException();
        }

        private static void IsPageShortNameRegisteredAsyncEnd(HttpResponseMessage response, Action<bool> clientCallback)
        {
            var pageIsRegistered = ProcessIsPageShortNameRegisteredResponse(response);
            clientCallback(pageIsRegistered);
        }

        private static bool ProcessIsPageShortNameRegisteredResponse(HttpResponseMessage response)
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

        public void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName + "/images" + "?caption=" + Uri.EscapeDataString(caption);
            var response = Parent.HttpChannel.PerformRawRequest("POST", locationFormat, imageContentType, imageBytes); 
            
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return;
                default:
                    var rawResponse = response.Content.Content;
                    var potentialErrors = Parent.HttpChannel.TryExtractErrorsFromResponse(rawResponse);
                    throw ErrorResponseExceptionFactory.CreateException(response, potentialErrors);
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

        private string GetRetrieveDonationsForPageLocationFormat(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName + "/donations";
            locationFormat += "?PageSize=" + pageSize.GetValueOrDefault(50);
            locationFormat += "&PageNum=" + pageNumber.GetValueOrDefault(1);
            return locationFormat;
        }

        private string GetCreateLocationFormat(RegisterPageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Request cannot be null.");
            }

            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages";
        }

        private string GetUpdateStoryLocationFormat(string pageShortName)
        {
            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName;
        }
        
        private string GetIsPageShortNameRegisteredLocationFormat(string pageShortName)
        {
            if (string.IsNullOrEmpty(pageShortName))
            {
                throw new ArgumentNullException("pageShortName", "pageShortName cannot be null.");
            }

            return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising/pages/" + pageShortName;
        }
    }
}