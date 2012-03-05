using System.Collections.Generic;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IPageApi: IPageApiAsync
    {
        FundraisingPageSummaries ListAll();
        FundraisingPage Retrieve(string pageShortName);
        FundraisingPageDonations RetrieveDonationsForPage(string pageShortName);
        FundraisingPageDonations RetrieveDonationsForPage(string pageShortName, int? pageSize, int? pageNumber);
        PageRegistrationConfirmation Create(RegisterPageRequest request);
        SuggestedNames SuggestPageShortNames(string preferedName);
        void UpdateStory(string pageShortName, string storyUpdate);
        bool IsPageShortNameRegistered(string pageShortName, string domain);
        void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType);
        AddFundraisingPageImageConfirmation AddImage(AddFundraisingPageImageRequest request);
        AddFundraisingPageVideoConfirmation AddVideo(AddFundraisingPageVideoRequest request);
        FundraisingPageImages GetImages(GetFundraisingPageImagesRequest request);
        FundraisingPageVideos GetVideos(GetFundraisingPageVideosRequest request);
    }
}