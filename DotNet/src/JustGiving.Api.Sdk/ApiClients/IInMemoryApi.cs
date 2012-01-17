using JustGiving.Api.Sdk.Model.Remember;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IInMemoryApi: IInMemoryApiAsync
    {
        RememberedPersonResponse Retrieve(int rememberedPersonId);
        RememberedPersonCollection RetrieveCollectionData(int rememberedPersonId);
    }
}