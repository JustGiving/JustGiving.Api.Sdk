using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class CurrencyApi : ApiClientBase, ICurrencyApi
    {
        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}/fundraising/currencies"; }
        }

        public CurrencyApi(HttpChannel channel)
            : base(channel)
        {
        }

        public Currencies ValidCurrencyCodes()
        {
            var resourceEndpoint = ResourceBase;
            var result = HttpChannel.PerformRequest<Currencies>("GET", resourceEndpoint);
            return result;
        }

        public void ValidCurrencyCodesAsync(Action<Currencies> callback)
        {
            var resourceEndpoint = ResourceBase;
            HttpChannel.GetAsync<Currencies>(resourceEndpoint, callback);

        }

        [DataContract(Name = "currency", Namespace = "")]
        public class Currency
        {
            [DataMember(Name = "currencyCode")]
            public string CurrencyCode { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "currencySymbol")]
            public string CurrencySymbol { get; set; }
        }

        [CollectionDataContract(Name = "currencies", ItemName = "currency", Namespace = "")]
        public class Currencies : List<Currency>
        {
            
        }
    }
}
