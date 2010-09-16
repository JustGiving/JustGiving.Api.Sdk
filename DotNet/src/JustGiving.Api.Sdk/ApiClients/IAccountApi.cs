using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IAccountApi
    {
        string Create(CreateAccountRequest request);
        FundraisingPageSummarys ListAllPages(string email);
    }
}