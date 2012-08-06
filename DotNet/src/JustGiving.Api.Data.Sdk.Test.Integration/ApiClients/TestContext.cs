using System;
using JustGiving.Api.Data.Sdk.Configuration;
using JustGiving.Api.Data.Sdk.Test.Integration.Configuration;
using JustGiving.Api.Sdk.Test.Common.Configuration;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    public class TestContext
    {

        public static string ApiLocation
        {
            get { return DataSdkConfigurationManager.GetProperty(x => x.RootDomain); }
        }

        public static string ApiKey
        {
            get { return DataSdkConfigurationManager.GetProperty(x => x.ApiKey); }
        }

        public static string TestUsername
        {
            get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, string>(x => x.TestUserName); }
        }

        public static string TestValidPassword
        {
            get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, string>(x => x.TestUserPassword); }
        }

        //"nmjhpq32"; //staging - "zqfed068";

        public static DateTime StartDate
        {
            get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, DateTime>(x => x.StartDate); }
        }

        public const string Base64 = "ZWhhZXZhai5oYnZlZkBuZ2F6c3pxb3F0Lm9ieC54bTp6cWZlZDA2OA==";
        public const string TestInvalidPassword = "badPassword";
        public const int KnownDonationPaymentId = 1062979;
        public const int KnownGiftAidPaymentId = 2428911;
        
        public const int KnownPageId = 3621516;
        public const int KnownEventId = 805390;
        public static int KnownDonationPaymentId { get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.KnownDonationPaymentId); } }

//local;
        
        public static int KnownGiftAidPaymentId {get {    return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.KnownGiftAidPaymentId);}}
            //2475911; //local - 2428911;
        
        public const string GemBoxSerial = "REMOVED"; //see https://github.com/github/dmca/blob/master/2014-04-04-Gembox.md
        //public static int KnownPageId { get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.KnownPageId); } } //local : 3621516;
        public static int KnownEventId {get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.KnownEventIdForCustomCodes); }} //    = 824405; //local - 805390;
        public static int KnownEventIdForEventCustomCodes { get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.CustomCodeEventId); } }

        public static int KnownPageIdWithCustomCodes { get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.CustomCodePageId); } }
        public static int KnownEventIdWithPage { get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, int>(x => x.EventId); } }
        
        public static DateTime PageCreatedStartDate
        {
            get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, DateTime>(x => x.PageCreatedStartDate); }
        }

        public static bool PageStatus
        {
            get { return TestConfigurationsHelper.GetProperty<ITestConfigurations, bool>(x => x.PageStatus); }
            set { throw new NotImplementedException(); }
        }

        public const string KnownEventCustomCode1 = "value1";
        public const string KnownEventCustomCode2 = "value2";
        public const string KnownEventCustomCode3 = "value3";
        public const string KnownPageCustomCode1 = "Mrs";
        public const string KnownPageCustomCode2 = "Sandra";
        public const string KnownPageCustomCode3 = "Osborne";
        public const string KnownPageCustomCode4 = "8GarrickRoad";
        public const string KnownPageCustomCode5 = "Blah";
        public const string KnownPageCustomCode6 = "Northampton";
        public const string KnownAppealName = "demo: General Appeal";
        public static DateTime KnownStartDateForPageSearch = new DateTime(2012, 03, 01);
        public static DateTime KnownEndDateForPageSearch = new DateTime(2012, 05, 01);
        public static readonly DateTime KnownExpiryDate = new DateTime(2006, 11, 17);
        public static readonly DateTime KnownEventDate = new DateTime(2006, 9, 9);
        public static DateTime ValidPageSearchStartDate = TestConfigurationsHelper.GetProperty<ITestConfigurations, DateTime>(x => x.ValidPageSearchStartDate);
        public static DateTime ValidPageSearchEndDate = TestConfigurationsHelper.GetProperty<ITestConfigurations, DateTime>(x => x.ValidPageSearchEndDate);
    }


}
