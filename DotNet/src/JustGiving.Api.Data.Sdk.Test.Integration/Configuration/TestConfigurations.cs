using System;
using System.ComponentModel;
using System.Configuration;
using JustGiving.Api.Sdk.Test.Common.Configuration;

namespace JustGiving.Api.Data.Sdk.Test.Integration.Configuration
{
    public class TestConfigurations : ConfigurationSection, ITestConfigurations
    {
        [TypeConverter(typeof(StringToEnGBDateTimeConverter))]
        [ConfigurationProperty("startDate")]
        public DateTime StartDate
        {
            get { return (DateTime)this["startDate"]; }
            set { this["startDate"] = value; }
        }
      }
}