using System;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ICharityApiAsync
    {
        void RetrieveAsync(int charityId, Action<Charity> callback);
    }
}