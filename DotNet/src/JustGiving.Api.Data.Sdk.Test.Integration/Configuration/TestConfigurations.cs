using System;
using System.ComponentModel;
using System.Configuration;
using JustGiving.Api.Sdk.Test.Common.Configuration;

namespace JustGiving.Api.Data.Sdk.Test.Integration.Configuration
{
    public class TestConfigurations : ConfigurationSection, ITestConfigurations
    {
        [TypeConverter(typeof (StringToEnGBDateTimeConverter))]
        [ConfigurationProperty("StartDate")]
        public DateTime StartDate
        {
            get { return (DateTime) this["StartDate"]; }
            set { this["StartDate"] = value; }
        }


        [ConfigurationProperty("TestUserName")]
        public string TestUserName
        {
            get { return (string) this["TestUserName"]; }
            set { this["TestUserName"] = value; }
        }

        [ConfigurationProperty("TestUserPassword")]
        public string TestUserPassword
        {
            get { return (string) this["TestUserPassword"]; }
            set { this["TestUserPassword"] = value; }
        }

        [ConfigurationProperty("EventId")]
        public int EventId
        {
            get { return (int) this["EventId"]; }
            set { this["EventId"] = value; }
        }

        [TypeConverter(typeof (StringToEnGBDateTimeConverter))]
        [ConfigurationProperty("PageCreatedStartDate")]
        public DateTime PageCreatedStartDate
        {
            get { return (DateTime) this["PageCreatedStartDate"]; }
            set { this["EventId"] = value; }
        }

        [ConfigurationProperty("CustomCodePageId")]
        public int CustomCodePageId
        {
            get { return (int) this["CustomCodePageId"]; }
            set { this["CustomCodePageId"] = value; }
        }

        [ConfigurationProperty("CustomCodeEventId")]
        public int CustomCodeEventId
        {
            get { return (int) this["CustomCodeEventId"]; }
            set { this["CustomCodeEventId"] = value; }
        }

        [ConfigurationProperty("PageStatus")]
        public bool PageStatus
        {
            get { return (bool) this["PageStatus"]; }
            set { this["PageStatus"] = value; }

        }
        [ConfigurationProperty("KnownGiftAidPaymentId")]
        public int KnownGiftAidPaymentId
        {
            get { return (int)this["KnownGiftAidPaymentId"]; }
            set { this["KnownGiftAidPaymentId"] = value; }
        }

        [ConfigurationProperty("KnownDonationPaymentId")]
        public int KnownDonationPaymentId { 
            get { return (int) this["KnownDonationPaymentId"]; }
            set { this["KnownDonationPaymentId"] = value; }
        }

        [ConfigurationProperty("KnownEventIdForCustomCodes")]
        public int KnownEventIdForCustomCodes
        {
            get { return (int) this["KnownEventIdForCustomCodes"]; }
            set { this["KnownEventIdForCustomCodes"] = value; }
        }

        [ConfigurationProperty("KnownPageId")]
        public int KnownPageId
        {
            get { return (int) this["KnownPageId"]; }
            set { this["KnownPageId"] = value; }
        }

        [ConfigurationProperty("ValidPageSearchStartDate")]
        public DateTime ValidPageSearchStartDate
        {
            get { return (DateTime) this["ValidPageSearchStartDate"]; }
            set { this["ValidPageSearchStartDate"] = value; }
        }

        [ConfigurationProperty("ValidPageSearchEndDate")]
        public DateTime ValidPageSearchEndDate
        {
            get { return (DateTime) this["ValidPageSearchEndDate"]; }
            set { this["ValidPageSearchEndDate"] = value; }
        }
    }
}