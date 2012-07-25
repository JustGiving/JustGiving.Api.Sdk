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

        [ConfigurationProperty("apiLocation")]
        public string ApiLocation
        {
            get { return (string) this["apiLocation"]; }
            set { this["apiLocation"] = value; }
        }

        [ConfigurationProperty("testUserName")]
        public string TestUserName
        {
            get { return (string) this["testUserName"]; }
            set { this["testUserName"] = value; }
        }

        [ConfigurationProperty("testUserPassword")]
        public string TestUserPassword
        {
            get { return (string)this["testUserPassword"]; }
            set { this["testUserPassword"] = value; }
        }
    }
}