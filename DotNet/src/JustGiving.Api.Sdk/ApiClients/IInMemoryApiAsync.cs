using System;
using JustGiving.Api.Sdk.Model.Remember;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IInMemoryApiAsync
    {
        void RetrieveAsync(int rememberedPersonId, Action<RememberedPersonResponse> callback);
        void RetrieveCollectionDataAsync(int rememberedPersonId, Action<RememberedPersonResponse> callback);
    }
}