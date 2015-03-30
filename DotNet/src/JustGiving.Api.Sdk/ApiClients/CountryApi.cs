using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CountryApi : ApiClientBase, ICountryApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/countries"; }
        }

        public CountryApi(HttpChannel channel)
            : base(channel)
        {
        }
    }
}
