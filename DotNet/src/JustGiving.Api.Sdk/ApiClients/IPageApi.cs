using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IPageApi
    {
        FundraisingPageSummaries ListAll();
        FundraisingPage RetrievePage(string pageShortName);
        FundraisingPageDonations RetrieveDonationsForPage(string pageShortName);
        FundraisingPageDonations RetrieveDonationsForPage(string pageShortName, int? pageSize, int? pageNumber);
        PageRegistrationConfirmation Create(RegisterPageRequest pageRegistrationRequest);
        void UpdateStory(string pageShortName, string storyUpdate);
        bool IsPageShortNameRegistered(string pageShortName);
        void UploadImage(string pageShortName, string caption, byte[] imageBytes, string imageContentType);
    }
}