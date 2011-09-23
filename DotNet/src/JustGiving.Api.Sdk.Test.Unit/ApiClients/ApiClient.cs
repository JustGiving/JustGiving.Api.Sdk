using System;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    public static class ApiClient
    {
        public static TApiClient Create <TApiClient, TResponse>(MockHttpClient<TResponse> httpClient) where TApiClient : ApiClientBase
            where TResponse : class, new()
        {
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion);
            return Create<TApiClient, TResponse>(config, httpClient);
        }

        public static TApiClient Create <TApiClient, TResponse>(ClientConfiguration configuration, MockHttpClient<TResponse> httpClient) where TApiClient : ApiClientBase
            where TResponse : class, new()
        {
            var parent = new JustGivingClient(configuration, httpClient);
            var rtn = Activator.CreateInstance(typeof(TApiClient), parent.HttpChannel) as TApiClient;
            return rtn;
        }
    }
}
