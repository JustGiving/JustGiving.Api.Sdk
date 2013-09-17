using System;

namespace JustGiving.Api.Sdk
{
    public class ClientConfiguration
    {
        public string ApiKey { get; set; }
        public int ApiVersion { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RootDomain { get; set; }
        public WireDataFormat WireDataFormat { get; set; }
        public string WhiteLabelDomain { get; set; }
        public bool IsZipSupportedByClient { get; set; }
        public TimeSpan? ConnectionTimeOut { get; set; }

        public ClientConfiguration(string apiKey): this("https://api.justgiving.com/", apiKey, 1)
        { 
        }
        public ClientConfiguration(string rootDomain, string apiKey, int apiVersion)
        {
            if (string.IsNullOrEmpty(rootDomain))
            {
                throw new ArgumentNullException("rootDomain", "rootDomain is required.");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey", "apiKey is required.");
            }

            if(apiVersion <= 0)
            {
                throw new ArgumentOutOfRangeException("apiVersion", "apiVersion must be valid.");
            }

            ApiKey = apiKey;
            ApiVersion = apiVersion;
            RootDomain = rootDomain;
            IsZipSupportedByClient = false;
        }
    }
}
