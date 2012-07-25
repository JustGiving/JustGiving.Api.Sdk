using System;

namespace JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions
{
    public static class DataClientConfigurationExtensions
    {
        public static DataClientConfiguration With(this DataClientConfiguration client, Action<DataClientConfiguration> action)
        {
            action(client);
            return client;
        }
    }
}