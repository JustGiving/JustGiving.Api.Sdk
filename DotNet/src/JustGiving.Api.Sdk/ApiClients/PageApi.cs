using System;
using System.Net;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class PageApi : ApiClientBase, IPageApi
	{
		public override string ResourceBase
		{
			get { return Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/fundraising"; }
		}

        public PageApi(JustGivingClientBase parent) : base(parent)
        {
        }
        
        public string ListAllLocationFormat()
        {
            if (string.IsNullOrEmpty(Parent.Configuration.Username) || string.IsNullOrEmpty(Parent.Configuration.Password))
            {
                throw new Exception("Authentication required to list pages.  Please set a valid configuration object.");
            }

            return ResourceBase + "/pages";
        }

        public FundraisingPageSummaries ListAll()
        {
            var locationFormat = ListAllLocationFormat();
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageSummaries>("GET", locationFormat);
        }

        public void ListAllAsync(Action<FundraisingPageSummaries> callback)
        {
            var locationFormat = ListAllLocationFormat();
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public string RetrieveLocationFormat(string pageShortName)
        {
            return ResourceBase +  "/pages/" + pageShortName;
        }

        public FundraisingPage Retrieve(string pageShortName)
        {
            var locationFormat = RetrieveLocationFormat(pageShortName);
            return Parent.HttpChannel.PerformApiRequest<FundraisingPage>("GET", locationFormat);
        }

        public void RetrieveAsync(string pageShortName, Action<FundraisingPage> callback)
        {
            var locationFormat = RetrieveLocationFormat(pageShortName);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public string RetrieveDonationsForPageLocationFormat(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = ResourceBase +  "/pages/" + pageShortName + "/donations";
            locationFormat += "?PageSize=" + pageSize.GetValueOrDefault(50);
            locationFormat += "&PageNum=" + pageNumber.GetValueOrDefault(1);
            return locationFormat;
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName)
        {
            return RetrieveDonationsForPage(pageShortName, 50, 1);
        }

        public FundraisingPageDonations RetrieveDonationsForPage(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = RetrieveDonationsForPageLocationFormat(pageShortName, pageSize, pageNumber);
            return Parent.HttpChannel.PerformApiRequest<FundraisingPageDonations>("GET", locationFormat);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, Action<FundraisingPageDonations> callback)
        {
            RetrieveDonationsForPageAsync(pageShortName, 50, 1, callback);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, int? pageSize, int? pageNumber, Action<FundraisingPageDonations> callback)
        {
            var locationFormat = RetrieveDonationsForPageLocationFormat(pageShortName, pageSize, pageNumber);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, callback);
        }

        public string CreateLocationFormat(RegisterPageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Request cannot be null.");
            }

            return ResourceBase + "/pages";
        }

        public PageRegistrationConfirmation Create(RegisterPageRequest request)
        {
            string locationFormat = CreateLocationFormat(request);
            return Parent.HttpChannel.PerformApiRequest<RegisterPageRequest, PageRegistrationConfirmation>("PUT", locationFormat, request);
        }

        public PageRegistrationByEventRefConfirmation Create(string eventRef, RegisterPageRequest request)
        {
            string locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/ref/" + eventRef + "/pages";
            return Parent.HttpChannel.PerformApiRequest<RegisterPageRequest, PageRegistrationByEventRefConfirmation>("POST", locationFormat, request);
        }

        public void CreateAsync(RegisterPageRequest request, Action<PageRegistrationConfirmation> callback)
        {
            var locationFormat = CreateLocationFormat(request);
            Parent.HttpChannel.PerformApiRequestAsync("PUT", locationFormat, request, callback);
        }

        public void CreateAsync(string eventRef, RegisterPageRequest request, Action<PageRegistrationByEventRefConfirmation> callback)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/event/ref/" + eventRef + "/pages";
            Parent.HttpChannel.PerformApiRequestAsync("POST", locationFormat, request, callback);
        }

        public string UpdateStoryLocationFormat(string pageShortName)
        {
            return ResourceBase +  "/pages/" + pageShortName;
        }

        public void UpdateStory(string pageShortName, string storyUpdate)
        {
            var locationFormat = UpdateStoryLocationFormat(pageShortName);
            Parent.HttpChannel.PerformApiRequest<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate });
        }

        public void UpdateStoryAsync(string pageShortName, string storyUpdate)
        {
            var locationFormat = UpdateStoryLocationFormat(pageShortName);
            Parent.HttpChannel.PerformApiRequestAsync<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate }, response=>{});
        }

        public string IsPageShortNameRegisteredLocationFormat(string pageShortName, string domain)
        {
            if (string.IsNullOrEmpty(pageShortName))
                throw new ArgumentNullException("pageShortName", "pageShortName cannot be null.");

            if (!string.IsNullOrEmpty(domain))
                domain = string.Format("?domain={0}",domain);

            return ResourceBase + "/pages/" + pageShortName + domain;
        }

        public bool IsPageShortNameRegistered(string pageShortName, string domain)
        {
            var locationFormat = IsPageShortNameRegisteredLocationFormat(pageShortName, domain);
            var response = Parent.HttpChannel.PerformRawRequest("HEAD", locationFormat);
            return ProcessIsPageShortNameRegisteredResponse(response);
        }

        public bool IsPageShortNameRegistered(string pageShortName) { return IsPageShortNameRegistered(pageShortName, null); }

        public void IsPageShortNameRegisteredAsync(string pageShortName, string domain, Action<bool> callback)
        {
            var locationFormat = IsPageShortNameRegisteredLocationFormat(pageShortName, domain);
            Parent.HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response=>IsPageShortNameRegisteredAsyncEnd(response, callback));
        }

        public void IsPageShortNameRegisteredAsync(string pageShortName, Action<bool> callback) { IsPageShortNameRegisteredAsync(pageShortName, null, callback); }

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

        public string UploadImageLocationFormat(string pageShortName, string caption)
        {
            return ResourceBase + "/pages/" + pageShortName + "/images" + "?caption=" + Uri.EscapeDataString(caption);
        }

        public void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType)
        {
            var locationFormat = UploadImageLocationFormat(pageShortName, caption);
            var response = Parent.HttpChannel.PerformRawRequest("POST", locationFormat, imageContentType, imageBytes); 
            ProcessUploadImageResponse(response);
        }

        public AddFundraisingPageImageConfirmation AddImage(AddFundraisingPageImageRequest request)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            return
                Parent.HttpChannel.PerformApiRequest
                    <AddFundraisingPageImageRequest, AddFundraisingPageImageConfirmation>("PUT", locationFormat, request);
        }

        public AddFundraisingPageVideoConfirmation AddVideo(AddFundraisingPageVideoRequest request)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            return
                Parent.HttpChannel.PerformApiRequest
                    <AddFundraisingPageVideoRequest, AddFundraisingPageVideoConfirmation>("PUT", locationFormat, request);
        }

        public FundraisingPageImages GetImages(GetFundraisingPageImagesRequest request)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            return Parent.HttpChannel.PerformApiRequest<GetFundraisingPageImagesRequest, FundraisingPageImages>("GET", locationFormat, request);
        }

        public FundraisingPageVideos GetVideos(GetFundraisingPageVideosRequest request)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            return Parent.HttpChannel.PerformApiRequest<GetFundraisingPageVideosRequest, FundraisingPageVideos>("GET", locationFormat, request);
        }

        private string FundraisingPageImagesLocationFormat(string pageShortName)
        {
            return ResourceBase + "/pages/" + pageShortName + "/images";
        }
        private string FundraisingPageVideosLocationFormat(string pageShortName)
        {
			return ResourceBase + "/pages/" + pageShortName + "/videos";
        }

        public void UploadImageAsync(string pageShortName, string caption, byte[] imageBytes, string imageContentType)
        {
            throw new InvalidOperationException("UploadImageAsync not yet complete in SDK.");

            var locationFormat = UploadImageLocationFormat(pageShortName, caption);
            Parent.HttpChannel.PerformRawRequestAsync("POST", locationFormat, imageContentType, imageBytes, UploadImageAsyncEnd);
        }

        public void AddImageAsync(AddFundraisingPageImageRequest request, Action<AddFundraisingPageImageConfirmation> callback)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            Parent.HttpChannel.PerformApiRequestAsync("PUT", locationFormat, request, callback);
        }

        public void AddVideoAsync(AddFundraisingPageVideoRequest request, Action<AddFundraisingPageVideoConfirmation> callback)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            Parent.HttpChannel.PerformApiRequestAsync("PUT", locationFormat, request, callback);
        }

        public void GetImagesAsync(GetFundraisingPageImagesRequest request, Action<FundraisingPageImages> callback)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            Parent.HttpChannel.PerformApiRequestAsync("GET",locationFormat,request,callback);
        }

        public void GetVideosAsync(GetFundraisingPageVideosRequest request, Action<FundraisingPageVideos> callback)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            Parent.HttpChannel.PerformApiRequestAsync("GET", locationFormat, request, callback);
        }

        private void UploadImageAsyncEnd(HttpResponseMessage response)
        {
            ProcessUploadImageResponse(response);
        }

        private void ProcessUploadImageResponse(HttpResponseMessage response)
        {
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
    }
}