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
        void UpdateStory(string pageShortName, string storyUpdate);
        bool IsPageShortNameRegistered(string pageShortName);
        void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType);
    }
}