using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.Charity
{
    [DataContract(Name = "mobileAppeal", Namespace = "")]
    public class MobileAppeal
    {
        [DataMember(Name = "smsCode")]
        public string SmsCode { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
