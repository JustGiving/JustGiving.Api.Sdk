using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk
{
    public interface IJustGivingClient
    {
        IAccountApi Account { get; set; }
        IDonationApi Donation { get; set; }
        IPageApi Page { get; set; }
        ISearchApi Search { get; set; }
        ICharityApi Charity { get; set; }
        IEventApi Event { get; set; }
        ITeamApi Team { get; set; }
        IOneSearchApi OneSearch { get; set; }
        string WhiteLabelDomain { get; }
        IHttpClient HttpClient { get; }
        HttpChannel HttpChannel { get; }
        void SetWhiteLabelDomain(string domain);
        void InitApis(IHttpClient httpClient, ClientConfiguration clientConfiguration);
        void UpdateConfiguration(ClientConfiguration configuration);
    }
}