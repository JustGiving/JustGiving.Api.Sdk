using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ICountryApiAsync
    {
        void CountriesAsync(Action<CountryApi.CountryCollection> callback);
    }
}
