using System;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IAccountApiAsync
    {
        void AreCredentialsValidAsync(string email, string password, Action<bool> callback);
        void CreateAsync(CreateAccountRequest request, Action<string> callback);
        void ListAllPagesAsync(string email, Action<FundraisingPageSummaries> callback);
        void RequestPasswordReminderAsync(string email);
        void IsEmailRegisteredAsync(string email, Action<bool> callback);
        void ContentRatingHistoryAsync(Action<AccountApi.ContentRatingHistoryResponse> callback);
        void InterestAsync(Action<AccountApi.InterestResponse> callback);
    }
}