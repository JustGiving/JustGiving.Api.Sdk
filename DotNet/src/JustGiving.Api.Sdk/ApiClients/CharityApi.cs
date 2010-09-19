using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.Model.Charity;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CharityApi: ApiClientBase, ICharityApi
    {
        public CharityApi(JustGivingClient parent) : base(parent)
        {
        }

        public Charity RetrieveCharity(int charityId)
        {
            var locationFormat = Parent.Configuration.RootDomain + "{apiKey}/v{apiVersion}/charity/" + charityId;
            return Parent.HttpChannel.PerformApiRequest<Charity>("GET", locationFormat);
        }
    }
}
