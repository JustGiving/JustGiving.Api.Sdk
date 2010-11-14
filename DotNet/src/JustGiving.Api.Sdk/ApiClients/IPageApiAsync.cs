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
        void UpdateStoryAsync(string pageShortName, string storyUpdate);
        void IsPageShortNameRegisteredAsync(string pageShortName, Action<bool> callback);
        void UploadImageAsync(string pageShortName, string caption, byte[] imageBytes, string imageContentType);
    }
}