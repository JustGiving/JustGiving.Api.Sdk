using System;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    public class TestContext
    {
        public const string ApiLocation = "http://dataapi.local.justgiving.com/";
        public const string ApiKey = "decbf1d2";
        public const string TestUsername = "ehaevaj.hbvef@ngazszqoqt.obx.xm";
        public const string TestValidPassword = "zqfed068";
        public const string Base64 = "ZWhhZXZhai5oYnZlZkBuZ2F6c3pxb3F0Lm9ieC54bTp6cWZlZDA2OA==";
        public const string TestInvalidPassword = "badPassword";
        public const int KnownDonationPaymentId = 1062979;
        public const int KnownGiftAidPaymentId = 1535205;
        public const string GemBoxSerial = "REMOVED"; //see https://github.com/github/dmca/blob/master/2014-04-04-Gembox.md
        public const int KnownPageId = 2739376;
        public const int KnownCharityId = 50;
        public const int KnownEventId = 11493;
        public const int KnownEventIdWithPage = 498800;
        public const string KnownEventCustomCode1 = "value1";
        public const string KnownEventCustomCode2 = "value2";
        public const string KnownEventCustomCode3 = "value3";
        public const string KnownPageCustomCode1 = "Mrs";
        public const string KnownPageCustomCode2 = "Sandra";
        public const string KnownPageCustomCode3 = "Osborne";
        public const string KnownPageCustomCode4 = "8 Garrick Road";
        public const string KnownPageCustomCode5 = "n/a";
        public const string KnownPageCustomCode6 = "Northampton";
        public const string KnownAppealName = "mariecurie: General Appeal";
        public static readonly DateTime KnownExpiryDate = new DateTime(2006, 11, 17);
        public static readonly DateTime KnownEventDate = new DateTime(2006, 9, 9);
    } 
}
