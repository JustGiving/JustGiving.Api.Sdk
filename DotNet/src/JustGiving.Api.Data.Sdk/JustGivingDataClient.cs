using JustGiving.Api.Data.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;
using JustGiving.Api.Sdk.Http.MicrosoftHttp;

namespace JustGiving.Api.Data.Sdk
{
    public class JustGivingDataClient 
    {
        private readonly DataClientConfiguration _dataClientConfiguration;
        private readonly IHttpClient _httpClient;
        public HttpChannel HttpChannel { get; private set; }

        private IPaymentsApi _payments;
        public IPaymentsApi Payments
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

            _payments = new PaymentsApi(HttpChannel);
        }
    }
}