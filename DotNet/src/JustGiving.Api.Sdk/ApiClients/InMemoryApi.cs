using System;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Model.Remember;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class InMemoryApi: ApiClientBase, IInMemoryApi
	{
		public override string ResourceBase
		{
			get { return "{apiKey}/v{apiVersion}/remember"; }
		}

        public InMemoryApi(HttpChannel channel)
            : base(channel)
        {
        }

        public string RetrieveLocationFormat(int rememberedPersonId)
        {
            return ResourceBase + "/" + rememberedPersonId;
        }

        public RememberedPersonResponse Retrieve(int rememberedPersonId)
        {
            var locationFormat = RetrieveLocationFormat(rememberedPersonId) ;
            return HttpChannel.PerformRequest<RememberedPersonResponse>("GET", locationFormat);
        }

        public void RetrieveAsync(int rememberedPersonId, Action<RememberedPersonResponse> callback)
        {
            var locationFormat = RetrieveLocationFormat(rememberedPersonId);
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

        public RememberedPersonCollection RetrieveCollectionData(int rememberedPersonId)
        {
            var locationFormat = RetrieveLocationFormat(rememberedPersonId)+ "/collection";
            return HttpChannel.PerformRequest<RememberedPersonCollection>("GET", locationFormat);
        }

        public void RetrieveCollectionDataAsync(int rememberedPersonId, Action<RememberedPersonResponse> callback)
        {
            var locationFormat = RetrieveLocationFormat(rememberedPersonId) + "/collection";
            HttpChannel.PerformRequestAsync("GET", locationFormat, callback);
        }

    }
}
