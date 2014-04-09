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

        private IPaymentsApi _payment;
        private IPagesApi _pages;

        public IPaymentsApi Payment
        {
            get { return _payment; }
            private set { _payment = value; }
        }

        public IPagesApi Pages
        {
            get {return _pages;}
            private set {_pages = value;}
        }

        private ICustomCodesApi _customCodes;
        public ICustomCodesApi CustomCodes
        {
            get { return _customCodes; }
            set { _customCodes = value; }
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

            _payment = new PaymentsApi(HttpChannel);
            _pages = new PagesApi(HttpChannel);
            _customCodes = new CustomCodesApi(HttpChannel);
        }
    }
}