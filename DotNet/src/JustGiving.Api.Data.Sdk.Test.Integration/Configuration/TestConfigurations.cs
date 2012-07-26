using System;
using System.ComponentModel;
using System.Configuration;
using JustGiving.Api.Sdk.Test.Common.Configuration;

namespace JustGiving.Api.Data.Sdk.Test.Integration.Configuration
{
    public class TestConfigurations : ConfigurationSection, ITestConfigurations
    {
        [TypeConverter(typeof(StringToEnGBDateTimeConverter))]
        [ConfigurationProperty("StartDate")]
        public DateTime StartDate
        {
            get { return (DateTime)this["StartDate"]; }
            set { this["StartDate"] = value; }
        }


        [ConfigurationProperty("TestUserName")]
        public string TestUserName
        {
            get { return (string)this["TestUserName"]; }
            set { this["TestUserName"] = value; }
        }

        [ConfigurationProperty("TestUserPassword")]
        public string TestUserPassword
        {
            get { return (string)this["TestUserPassword"]; }
            set { this["TestUserPassword"] = value; }
        }
    }
}