using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ISmsApi : ISmsApiAsync
    {
        SmsApi.SmsInfo RetrievePageSmsCode(string pageShortName);
        SmsApi.SmsCodeAvailability CheckSmsCodeAvailability(string smsCode);
        bool UpdatePageSmsCode(string pageShortName, SmsApi.SmsUpdate smsUpdate);
    }
}
