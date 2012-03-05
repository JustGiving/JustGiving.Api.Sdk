using System;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IPageApiAsync
    {
        void ListAllAsync(Action<FundraisingPageSummaries> callback);
        void RetrieveAsync(string pageShortName, Action<FundraisingPage> callback);
        void RetrieveDonationsForPageAsync(string pageShortName, Action<FundraisingPageDonations> callback);
        void RetrieveDonationsForPageAsync(string pageShortName, int? pageSize, int? pageNumber, Action<FundraisingPageDonations> callback);
        void CreateAsync(RegisterPageRequest request, Action<PageRegistrationConfirmation> callback);
        void CreateAsync(string eventRef, RegisterPageRequest request, Action<PageRegistrationByEventRefConfirmation> callback);
        void SuggestPageShortNamesAsync(string preferedName, Action<SuggestedNames> callback);
        void UpdateStoryAsync(string pageShortName, string storyUpdate);
        void IsPageShortNameRegisteredAsync(string pageShortName, string domain, Action<bool> callback);
        void UploadImageAsync(string pageShortName, string caption, byte[] imageBytes, string imageContentType);
        void AddImageAsync(AddFundraisingPageImageRequest request, Action<AddFundraisingPageImageConfirmation> callback);
        void AddVideoAsync(AddFundraisingPageVideoRequest request, Action<AddFundraisingPageVideoConfirmation> callback);
        void GetImagesAsync(GetFundraisingPageImagesRequest request, Action<FundraisingPageImages> callback);
        void GetVideosAsync(GetFundraisingPageVideosRequest request, Action<FundraisingPageVideos> callback);
    }
}