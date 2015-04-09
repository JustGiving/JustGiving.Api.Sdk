using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class SmsApi : ApiClientBase, ISmsApi
    {
        public SmsApi(HttpChannel channel) : base(channel)
        {
        }

        public override string ResourceBase
        {
            get { return "{apiKey}/v{apiVersion}"; }
        }

        private string RetrievePageSmsCodeResourceEndpoint(string pageShortName)
        {
            return ResourceBase + "/fundraising/pages/" + pageShortName + "/sms";
        }

        public SmsInfo RetrievePageSmsCode(string pageShortName)
        {
            var resourceEndpoint = RetrievePageSmsCodeResourceEndpoint(pageShortName);
            var result = HttpChannel.PerformRequest<SmsInfo>("GET", resourceEndpoint);
            return result;
        }

        public void RetrievePageSmsCodeAsync(string pageShortName, Action<SmsInfo> callback)
        {
            var resourceEndpoint = RetrievePageSmsCodeResourceEndpoint(pageShortName);
            HttpChannel.PerformRequestAsync("GET", resourceEndpoint, callback);
        }

        public void CheckSmsCodeAvailabilityAsync(string smsCode, Action<SmsCodeAvailability> callback)
        {
            var resourceEndpoint = CheckSmsCodeAvailabilityResurceEndpoint(smsCode);
            var dummyRequest = new DummyRequest();
            HttpChannel.PostAsync(resourceEndpoint, dummyRequest, callback);
        }

        private string CheckSmsCodeAvailabilityResurceEndpoint(string smsCode)
        {
            return ResourceBase + "/sms/urn/" + smsCode + "/check";
        }

        public SmsCodeAvailability CheckSmsCodeAvailability(string smsCode)
        {
            var dummyRequest = new DummyRequest();
            var resourceEndpoint = CheckSmsCodeAvailabilityResurceEndpoint(smsCode);
            var result = HttpChannel.Post<DummyRequest, SmsCodeAvailability>(resourceEndpoint, dummyRequest);
            return result;
        }

        public class DummyRequest
        {}
        
        [DataContract(Namespace = "", Name = "smsInfo")]
        public class SmsInfo
        {
            [DataMember(Name = "urn")]
            public string Urn { get; set; }
        }

        [DataContract(Namespace = "", Name = "smsCodeAvailability")]
        public class SmsCodeAvailability
        {
            [DataMember(Name = "isAvailable")]
            public bool IsAvailable { get; set; }

            [DataMember(Name = "alternatives")]
            public string[] Alternatives { get; set; }


        }
    }
}
