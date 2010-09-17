using System;
using System.Text;

namespace JustGiving.Api.Sdk.Http
{
    public class HttpBasicAuthCredentials
    {
        private readonly string _user;
        private readonly string _pass;

        public HttpBasicAuthCredentials(string user, string pass)
        {
            if (string.IsNullOrEmpty(user))
                throw new ArgumentException(
                    "User is Required for Http Basic Auth", "user");

            if (string.IsNullOrEmpty(pass))
                throw new ArgumentException(
                    "Password is Required for Http Basic Auth", "pass");

            _user = user;
            _pass = pass;
        }

        public override string ToString()
        {
            var stringForEnc = string.Format("{0}:{1}", _user, _pass);
            var bytesForEnc = Encoding.UTF8.GetBytes(stringForEnc);
            return Convert.ToBase64String(bytesForEnc);
        }
    }
}