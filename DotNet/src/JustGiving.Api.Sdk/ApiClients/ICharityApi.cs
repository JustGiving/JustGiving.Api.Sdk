using System.Collections.Generic;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ICharityApi: ICharityApiAsync
    {
        Charity Retrieve(int charityId);
        CharityEvents RetrieveEvents(int charityId);
        CharityEvents RetrieveEvents(int charityId, int pageNumber, int pageSize);
    	CharityAuthenticationResult Authenticate(AuthenticateCharityUserRequest request);
        CharityApi.CharityDonationsResult CharityDonations(int charityId);
        CharityApi.CharityCategoriesResponse CharityCategories();
    }
}