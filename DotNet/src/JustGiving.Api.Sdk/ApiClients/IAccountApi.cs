using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IAccountApi: IAccountApiAsync
    {
        string Create(CreateAccountRequest request);
        FundraisingPageSummaries ListAllPages(string email);
    }
}