using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.DataPackets;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class PageApi : ApiClientBase, IPageApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/fundraising"; }
        }

        public PageApi(HttpChannel channel)
            : base(channel)
        {
        }

        public string ListAllLocationFormat()
        {
            if (string.IsNullOrEmpty(HttpChannel.ClientConfiguration.Username) || string.IsNullOrEmpty(HttpChannel.ClientConfiguration.Password))
            {
                throw new Exception("Authentication required to list pages.  Please set a valid configuration object.");
            }

            return ResourceBase + "/pages";
        }

        public string ListAllLocationPaginatedFormat()
        {
            if (string.IsNullOrEmpty(HttpChannel.ClientConfiguration.Username) || string.IsNullOrEmpty(HttpChannel.ClientConfiguration.Password))
            {
                throw new Exception("Authentication required to list pages.  Please set a valid configuration object.");
            }

            return ResourceBase + "/paginatedpages";
        }

        public FundraisingPageSummariesPaginated ListAll(int? page, int? pageSize, string inMemoryPersonSearch)
        {
            var locationFormat = ListAllLocationPaginatedFormat();
            locationFormat += "?page=" + page.GetValueOrDefault(1);
            locationFormat += "&pagesize=" + pageSize.GetValueOrDefault(Int32.MaxValue);

            if (inMemoryPersonSearch != null)
            {
                locationFormat += "&inMemoryPersonNameSearch=" + inMemoryPersonSearch;
            }
            return HttpChannel.PerformRequest<FundraisingPageSummariesPaginated>("GET", locationFormat);
        }

        public FundraisingPageSummaries ListAll()
        {
            var locationFormat = ListAllLocationFormat();
            return HttpChannel.PerformRequest<FundraisingPageSummaries>("GET", locationFormat);
        }

        public void ListAllAsync(int? page, int? pageSize, string inMemoryPersonSearch, Action<FundraisingPageSummariesPaginated> callback)
        {
            var locationFormat = ListAllLocationPaginatedFormat();
            locationFormat += "?page=" + page.GetValueOrDefault(1);
            locationFormat += "&pagesize=" + pageSize.GetValueOrDefault(Int32.MaxValue);

            if (inMemoryPersonSearch != null)
            {
                locationFormat += "&inMemoryPersonNameSearch=" + inMemoryPersonSearch;
            }
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public void ListAllAsync(Action<FundraisingPageSummaries> callback)
        {
            var locationFormat = ListAllLocationFormat();
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public string RetrieveLocationFormat(string pageShortName)
        {
            return ResourceBase + "/pages/" + pageShortName;
        }

        public FundraisingPage Retrieve(string pageShortName)
        {
            var locationFormat = RetrieveLocationFormat(pageShortName);
            return HttpChannel.PerformRequest<FundraisingPage>("GET", locationFormat);
        }

        public void RetrieveAsync(string pageShortName, Action<FundraisingPage> callback)
        {
            var locationFormat = RetrieveLocationFormat(pageShortName);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public string RetrieveDonationsForPageLocationFormat(string pageShortName, int? pageSize, int? pageNumber)
        {
            var locationFormat = ResourceBase + "/pages/" + pageShortName + "/donations";
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
            return HttpChannel.PerformRequest<FundraisingPageDonations>("GET", locationFormat);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, Action<FundraisingPageDonations> callback)
        {
            RetrieveDonationsForPageAsync(pageShortName, 50, 1, callback);
        }

        public void RetrieveDonationsForPageAsync(string pageShortName, int? pageSize, int? pageNumber, Action<FundraisingPageDonations> callback)
        {
            var locationFormat = RetrieveDonationsForPageLocationFormat(pageShortName, pageSize, pageNumber);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
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
            var locationFormat = CreateLocationFormat(request);
            return HttpChannel.PerformRequest<RegisterPageRequest, PageRegistrationConfirmation>("PUT", locationFormat, request);
        }

        public PageRegistrationByEventRefConfirmation Create(string eventRef, RegisterPageRequest request)
        {
            string locationFormat = "{apiKey}/v{apiVersion}/event/ref/" + eventRef + "/pages";
            return HttpChannel.PerformRequest<RegisterPageRequest, PageRegistrationByEventRefConfirmation>("POST", locationFormat, request);
        }

        public void CreateAsync(RegisterPageRequest request, Action<PageRegistrationConfirmation> callback)
        {
            var locationFormat = CreateLocationFormat(request);
            HttpChannel.PerformRequestAsync("PUT", locationFormat, request, callback);
        }

        public void CreateAsync(string eventRef, RegisterPageRequest request, Action<PageRegistrationByEventRefConfirmation> callback)
        {
            var locationFormat = "{apiKey}/v{apiVersion}/event/ref/" + eventRef + "/pages";
            HttpChannel.PerformRequestAsync("POST", locationFormat, request, callback);
        }

        public SuggestedNames SuggestPageShortNames(string preferedName)
        {
            var locationFormat = SuggestPageShortNameLocationFormat(preferedName);
            return HttpChannel.PerformRequest<SuggestedNames>("GET", locationFormat);
        }

        public void SuggestPageShortNamesAsync(string preferedName, Action<SuggestedNames> callback)
        {
            var locationFormat = SuggestPageShortNameLocationFormat(preferedName);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        private static string SuggestPageShortNameLocationFormat(string preferedName)
        {
            return "{apiKey}/v{apiVersion}/fundraising/pages/suggest?preferredName=" + Uri.EscapeDataString(preferedName);
        }

        public string UpdateStoryLocationFormat(string pageShortName)
        {
            return ResourceBase + "/pages/" + pageShortName;
        }

        public void UpdateStory(string pageShortName, string storyUpdate)
        {
            var locationFormat = UpdateStoryLocationFormat(pageShortName);
            HttpChannel.PerformRequest<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate });
        }

        public void UpdateStoryAsync(string pageShortName, string storyUpdate)
        {
            var locationFormat = UpdateStoryLocationFormat(pageShortName);
            HttpChannel.PerformRequestAsync<StoryUpdateRequest, StoryUpdateResponse>("POST", locationFormat, new StoryUpdateRequest { StorySupplement = storyUpdate }, response => { });
        }

        public string IsPageShortNameRegisteredLocationFormat(string pageShortName, string domain)
        {
            if (string.IsNullOrEmpty(pageShortName))
            {
                throw new ArgumentNullException("pageShortName", "pageShortName cannot be null.");
            }
            if (!string.IsNullOrEmpty(domain))
            {
                domain = string.Format("?domain={0}", domain);
            }

            return ResourceBase + "/pages/" + pageShortName + domain;
        }

        public bool IsPageShortNameRegistered(string pageShortName)
        {
            return IsPageShortNameRegistered(pageShortName, null);
        }

        public bool IsPageShortNameRegistered(string pageShortName, string domain)
        {
            var locationFormat = IsPageShortNameRegisteredLocationFormat(pageShortName, domain);
            var response = HttpChannel.PerformRawRequest("HEAD", locationFormat);
            return ProcessIsPageShortNameRegisteredResponse(response);
        }

        public void IsPageShortNameRegisteredAsync(string pageShortName, string domain, Action<bool> callback)
        {
            var locationFormat = IsPageShortNameRegisteredLocationFormat(pageShortName, domain);
            HttpChannel.PerformRawRequestAsync("HEAD", locationFormat, response => IsPageShortNameRegisteredAsyncEnd(response, callback));
        }

        public void IsPageShortNameRegisteredAsync(string pageShortName, Action<bool> callback)
        {
            IsPageShortNameRegisteredAsync(pageShortName, null, callback);
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

        public string UploadImageLocationFormat(string pageShortName, string caption)
        {
            return ResourceBase + "/pages/" + pageShortName + "/images" + "?caption=" + Uri.EscapeDataString(caption);
        }

        public void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType)
        {
            var locationFormat = UploadImageLocationFormat(pageShortName, caption);
            var response = HttpChannel.PerformRawRequest("POST", locationFormat, imageContentType, imageBytes);
            ProcessUploadImageResponse(response);
        }

        public AddFundraisingPageImageConfirmation AddImage(AddFundraisingPageImageRequest request)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            return
                HttpChannel.PerformRequest
                    <AddFundraisingPageImageRequest, AddFundraisingPageImageConfirmation>("PUT", locationFormat, request);
        }

        public AddFundraisingPageVideoConfirmation AddVideo(AddFundraisingPageVideoRequest request)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            return
                HttpChannel.PerformRequest
                    <AddFundraisingPageVideoRequest, AddFundraisingPageVideoConfirmation>("PUT", locationFormat, request);
        }

        public FundraisingPageImages GetImages(GetFundraisingPageImagesRequest request)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            return HttpChannel.PerformRequest<FundraisingPageImages>("GET", locationFormat);
        }

        public FundraisingPageVideos GetVideos(GetFundraisingPageVideosRequest request)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            return HttpChannel.PerformRequest<FundraisingPageVideos>("GET", locationFormat);
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
            HttpChannel.PerformRawRequestAsync("POST", locationFormat, imageContentType, imageBytes, ProcessUploadImageResponse);
        }

        public void AddImageAsync(AddFundraisingPageImageRequest request, Action<AddFundraisingPageImageConfirmation> callback)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            HttpChannel.PerformRequestAsync("PUT", locationFormat, request, callback);
        }

        public void AddVideoAsync(AddFundraisingPageVideoRequest request, Action<AddFundraisingPageVideoConfirmation> callback)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            HttpChannel.PerformRequestAsync("PUT", locationFormat, request, callback);
        }

        public void GetImagesAsync(GetFundraisingPageImagesRequest request, Action<FundraisingPageImages> callback)
        {
            var locationFormat = FundraisingPageImagesLocationFormat(request.PageShortName);
            HttpChannel.PerformRequestAsync("GET", locationFormat, request, callback);
        }

        public void GetVideosAsync(GetFundraisingPageVideosRequest request, Action<FundraisingPageVideos> callback)
        {
            var locationFormat = FundraisingPageVideosLocationFormat(request.PageShortName);
            HttpChannel.PerformRequestAsync("GET", locationFormat, request, callback);
        }

        private void ProcessUploadImageResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                return;

            var potentialErrors = HttpChannel.TryExtractErrorsFromResponse(response.Content);
            throw ErrorResponseExceptionFactory.CreateException(response, potentialErrors);
        }

        private string RetrieveDonationsForPageByReferenceLocationFormat(string pageShortName, string reference)
        {
            var locationFormat = ResourceBase + "/pages/" + pageShortName + "/donations" + "/ref/" + reference;
            return locationFormat;
        }

        public FundraisingPageDonations RetrieveDonationsForPageByReference(string pageShortName, string reference)
        {
            var locationFormat = RetrieveDonationsForPageByReferenceLocationFormat(pageShortName, reference);
            var result = HttpChannel.PerformRequest<FundraisingPageDonations>("GET", locationFormat);
            return result;
        }

        private string PageUpdatesLocationFormat(string pageShortName)
        {
            return ResourceBase + "/pages/" + pageShortName + "/updates";
        }

        public Updates PageUpdates(string pageShortName)
        {
            var resourceEndpoint = PageUpdatesLocationFormat(pageShortName);
            var result = HttpChannel.PerformRequest<Updates>("GET", resourceEndpoint);
            return result;
        }

        [CollectionDataContract(Name = "Updates", ItemName = "update", Namespace = "")]
        public class Updates : List<Update>
        {
        }

        [DataContract(Name = "Update", Namespace = "")]
        public class Update
        {
            [DataMember]
            public int? Id { get; set; }

            [DataMember]
            public string Video { get; set; }

            [DataMember]
            public DateTime? CreatedDate { get; set; }
            
            [DataMember]
            public string Message { get; set; }
        }
    }
}

