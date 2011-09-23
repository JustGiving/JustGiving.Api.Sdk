using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk
{
    public abstract class JustGivingClientBase
    {
        public IAccountApi Account { get; set; }
        public IDonationApi Donation { get; set; }
        public IPageApi Page { get; set; }
        public ISearchApi Search { get; set; }
        public ICharityApi Charity { get; set; }
        public IEventApi Event { get; set; }

        public string WhiteLabelDomain { get; private set; }
    	public IHttpClient HttpClient { get; private set; }

        protected internal ClientConfiguration Configuration { get; private set; }
        public HttpChannel HttpChannel { get; private set; }

        protected JustGivingClientBase(ClientConfiguration clientConfiguration, IHttpClient httpClient)
            : this(clientConfiguration, httpClient, null, null, null, null, null, null)
        {
        }

        protected JustGivingClientBase(ClientConfiguration clientConfiguration, IHttpClient httpClient, IAccountApi accountApi, IDonationApi donationApi, IPageApi pageApi, ISearchApi searchApi, ICharityApi charityApi, IEventApi eventApi)
        {
            if(httpClient == null)
            {
                throw new ArgumentNullException("httpClient", "httpClient must not be null to access the api.");
            }

            HttpClient = httpClient;

            Account = accountApi;
            Donation = donationApi;
            Page = pageApi;
            Search = searchApi;
            Charity = charityApi;
            Event = eventApi;

            Configuration = clientConfiguration;

            InitApis(HttpClient, clientConfiguration);
        }

        public void SetWhiteLabelDomain(string domain)
        {
            WhiteLabelDomain = domain;
            Configuration.WhiteLabelDomain = domain;
        }

        private void InitApis(IHttpClient httpClient, ClientConfiguration clientConfiguration)
        {
            HttpChannel = new HttpChannel(clientConfiguration, httpClient);

            if (Account == null)
            {
				Account = new AccountApi(HttpChannel);
            }

            if (Donation == null)
            {
				Donation = new DonationApi(HttpChannel);
            }

            if (Page == null)
            {
				Page = new PageApi(HttpChannel);
            }

            if (Search == null)
            {
				Search = new SearchApi(HttpChannel);
            }

            if(Charity == null)
            {
				Charity = new CharityApi(HttpChannel);
            }

            if(Event == null)
            {
				Event = new EventApi(HttpChannel);
            }
        }

        public void UpdateConfiguration(ClientConfiguration configuration)
        {
            Configuration = configuration;
            InitApis(HttpClient, configuration);
        }
    }
}
