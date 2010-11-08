using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CharityApi: ApiClientBase, ICharityApi
    {
        public CharityApi(JustGivingClientBase parent)
            : base(parent)
        {
        }

        public Charity Retrieve(int charityId)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/charity/" + charityId;
            return Parent.HttpChannel.PerformApiRequest<Charity>("GET", locationFormat);
        }
    }
}
