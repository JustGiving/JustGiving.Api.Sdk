using System;
using System.Configuration;
using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Data.Sdk.ApiClients
{
    public abstract class DataApiClientBase : ApiClientBase
    {
        private static DataClientConfiguration DataClientConfiguration { get; set; }

        protected DataApiClientBase(HttpChannel channel)
            : base(channel)
        {
            if (channel.ClientConfiguration == null)
            {
                DataClientConfiguration = new DataClientConfiguration();
            }
            else
            {
                DataClientConfiguration = channel.ClientConfiguration as DataClientConfiguration;
            }
        }

        protected DataApiClientBase(HttpChannel channel, ClientConfiguration clientConfiguration)
            : base(channel)
        {
            DataClientConfiguration = clientConfiguration as DataClientConfiguration;
        }

        protected static string BaseRoot
        {
            get
            {
                string baseRoot = "{apiKey}/v{apiVersion}";
                InitialiseConfiguration();

                if (DataClientConfiguration != null && DataClientConfiguration.CharityId != 0)
                {
                    baseRoot = baseRoot + "/charity/" + DataClientConfiguration.CharityId;
                }
                else
                {
                    throw new Exception("Couldn't get DataClientConfiguration, please double check if you have provided");
                }
                return baseRoot;
            }
        }

        private static void InitialiseConfiguration()
        {
            if (DataClientConfiguration == null)
            {
                DataClientConfiguration = new DataClientConfiguration();
            }
        }
    }
}
