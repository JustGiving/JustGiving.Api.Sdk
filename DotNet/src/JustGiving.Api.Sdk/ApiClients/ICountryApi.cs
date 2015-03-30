using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ICountryApi : ICountryApiAsync
    {
        CountryApi.CountryCollection Countries();
    }
}
