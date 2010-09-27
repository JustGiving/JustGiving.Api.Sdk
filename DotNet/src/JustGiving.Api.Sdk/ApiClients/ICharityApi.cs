using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ICharityApi
    {
        Charity Retrieve(int charityId);
    }
}