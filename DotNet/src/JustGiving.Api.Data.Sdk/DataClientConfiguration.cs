using System;
using JustGiving.Api.Sdk;

namespace JustGiving.Api.Data.Sdk
{
    public class DataClientConfiguration : ClientConfiguration
    {
        public DataClientConfiguration(string apiKey): base("http://dataapi.local.justgiving.com/", apiKey, 1)
        {
        }

        public DataClientConfiguration(string rootDomain, string apiKey, int apiVersion) : base(rootDomain, apiKey, apiVersion)
        {
        }
    }
}