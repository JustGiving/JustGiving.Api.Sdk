using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface ISmsApiAsync
    {
        void RetrievePageSmsCodeAsync(string pageShortName, Action<SmsApi.SmsInfo> callback);
        void CheckSmsCodeAvailabilityAsync(string smsCode, Action<SmsApi.SmsCodeAvailability> callback);
    }
}
