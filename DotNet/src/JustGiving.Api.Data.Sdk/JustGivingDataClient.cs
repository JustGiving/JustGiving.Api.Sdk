using System;
using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.MicrosoftHttp;

namespace JustGiving.Api.Data.Sdk
{
    public class JustGivingDataClient 
    {
        private readonly DataClientConfiguration _dataClientConfiguration;
        private readonly IHttpClient _httpClient;
        public HttpChannel HttpChannel { get; private set; }

        private IPayments _payments;
        public IPayments Payments
        {
            get { return _payments; }
            private set { _payments = value; }
        }

        public JustGivingDataClient(DataClientConfiguration dataClientConfiguration, IHttpClient httpClient)
        {
            _dataClientConfiguration = dataClientConfiguration;
            _httpClient = httpClient;

            InitialiseClients();
        }

        public JustGivingDataClient(DataClientConfiguration dataClientConfiguration)
        {
            _dataClientConfiguration = dataClientConfiguration;
            _httpClient = new HttpClientWrapper();

            InitialiseClients();
        }

        private void InitialiseClients()
        {
            HttpChannel = new HttpChannel(_dataClientConfiguration, _httpClient);

            _payments = new Payments(HttpChannel);
        }
    }

    public class DataClientConfiguration : ClientConfiguration
    {
        public bool IsZipSupportedByClient { get; set; }
        public TimeSpan? ConnectionTimeOut { get; set; }

        public DataClientConfiguration(string apiKey): base("https://dataapi.local.justgiving.com/", apiKey, 1)
        {
        }

        public DataClientConfiguration(string rootDomain, string apiKey, int apiVersion) : base(rootDomain, apiKey, apiVersion)
        {
        }
    }

    public interface IJustGivingDataClient
    {
        IHttpClient HttpClient { get; }
        HttpChannel HttpChannel { get; }
    }
}