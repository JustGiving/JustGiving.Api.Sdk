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
            return
                Create<TApiClient, TResponse>(
                    new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion),
                    httpClient);
        }

        public static TApiClient Create<TApiClient, TResponse>(ClientConfiguration config, MockHttpClient<TResponse> httpClient)
            where TApiClient : ApiClientBase
            where TResponse : class, new()
        {
            var parent = new JustGivingClient(config, httpClient);
            var rtn = Activator.CreateInstance(typeof(TApiClient), parent) as TApiClient;
            return rtn;
        }
    }
}