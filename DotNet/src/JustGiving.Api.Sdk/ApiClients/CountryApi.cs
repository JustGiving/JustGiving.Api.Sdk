using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        public CountryCollection Countries()
        {
            var resourceEndpoint = ResourceBase;
            var result = HttpChannel.PerformRequest<CountryCollection>("GET", resourceEndpoint);
            return result;
        }

        public void CountriesAsync(Action<CountryCollection> callback)
        {
            var resourceEndpoint = ResourceBase;
            HttpChannel.GetAsync(resourceEndpoint, callback);
        }

        [DataContract(Name = "country", Namespace = "")]
        public class Country
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "countryCode")]
            public string CountryCode { get; set; }
        }

        [CollectionDataContract(Name = "countries", ItemName = "country", Namespace = "")]
        public class CountryCollection : List<Country>
        {

        }
    }
}
