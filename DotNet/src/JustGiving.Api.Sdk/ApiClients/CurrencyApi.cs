using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CurrencyApi : ApiClientBase, ICurrencyApi
    {
        public override string ResourceBase
        {
            get { throw new NotImplementedException(); }
        }

        public CurrencyApi(HttpChannel channel)
            : base(channel)
        {
        }
    }
}
