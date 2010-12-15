using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.Account
{
    [DataContract(Namespace = "", Name = "requestPasswordReminderResponse")]
    public class PasswordReminderConfirmation
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
