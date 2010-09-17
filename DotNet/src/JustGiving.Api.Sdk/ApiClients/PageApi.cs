using System;
using System.Net;
using System.Security.Authentication;
using System.Web;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class PageApi : ApiClientBase, IPageApi
    {
        public PageApi(JustGivingClient parent)
            : base(parent)
        {
        }

        public FundraisingPageSummaries ListAll()
        {
            if(string.IsNullOrEmpty(Parent.Configuration.Username) || string.IsNullOrEmpty(Parent.Configuration.Password))
            {
                throw new AuthenticationException(
                    "Authentication required to list pages.  Please set a valid configuration object.");
            }
            // TODO (RC): Replace the "0" and "1" params with actual values ("apiVersion" and "apiKey", respectively).
            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages";
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummaries>("GET", locationFormat);
        }

        public FundraisingPage RetrievePage(string pageShortName)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages/" + pageShortName;
            return Parent.HttpChannel.PerformApiRequest<FundraisingPage>("GET", locationFormat);
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName)
        {
            return RetrieveDonationsForPage(pageShortName, 50, 1);
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages/" + pageShortName + "/donations";
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

            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages";
            return Parent.HttpChannel.PerformApiRequest<RegisterPageRequest, PageRegistrationConfirmation>("PUT", locationFormat, request);
        }

        public void UpdateStory(string pageShortName, string storyUpdate)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages/" + pageShortName;
            Parent.HttpChannel.PerformApiRequest<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate });
        }

        public bool IsPageShortNameRegistered(string pageShortName)
        {
            if(string.IsNullOrEmpty(pageShortName))
            {
                throw new ArgumentNullException("pageShortName", "pageShortName cannot be null.");
            }

            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages/" + pageShortName;
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
            var locationFormat = Parent.Configuration.RootDomain + "{0}/v{1}/fundraising/pages/" + pageShortName + "/images" + "?caption=" + HttpUtility.UrlEncode(caption);
            var response = Parent.HttpChannel.PerformRawRequest("POST", locationFormat, imageContentType, imageBytes); 
            
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return;
                default:
                    throw ErrorResponseExceptionFactory.CreateException(response, response.Content.ReadAsString(), null);
            }
        }
    }
}