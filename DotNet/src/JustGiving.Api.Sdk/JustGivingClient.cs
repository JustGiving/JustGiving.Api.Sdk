using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk
{
    public class JustGivingClient
    {
        public IAccountApi Account { get; set; }
        public IDonationApi Donation { get; set; }
        public IPageApi Page { get; set; }
        public ISearchApi Search { get; set; }

        internal ClientConfiguration Configuration { get; private set; }
        internal HttpChannel HttpChannel { get; private set; }

        public JustGivingClient(string apiKey)
            : this(new ClientConfiguration(apiKey), new HttpClientWrapper(), null, null, null, null)
        {
        }

        public JustGivingClient(ClientConfiguration clientConfiguration)
            : this(clientConfiguration, new HttpClientWrapper(), null, null, null, null)
        {
        }

        public JustGivingClient(ClientConfiguration clientConfiguration, IHttpClient httpClient)
            : this(clientConfiguration, httpClient, null, null, null, null)
        {
        }

        public JustGivingClient(ClientConfiguration clientConfiguration, IHttpClient httpClient, IAccountApi accountApi, IDonationApi donationApi, IPageApi pageApi, ISearchApi searchApi)
        {
            if(httpClient == null)
            {
                throw new ArgumentNullException("httpClient", "httpClient must not be null to access the api.");
            }

            Configuration = clientConfiguration;
            HttpChannel = new HttpChannel(clientConfiguration, httpClient);

            Account = accountApi;
            Donation = donationApi;
            Page = pageApi;
            Search = searchApi;

            if (Account == null)
            {
                Account = new AccountApi(this);
            }

            if (Donation == null)
            {
                Donation = new DonationApi(this);
            }

            if (Page == null)
            {
                Page = new PageApi(this);
            }

            if (Search == null)
            {
                Search = new SearchApi(this);
            }
        }
    }
}
