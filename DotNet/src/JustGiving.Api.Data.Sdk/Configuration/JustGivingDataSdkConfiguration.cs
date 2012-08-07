using System.Configuration;

namespace JustGiving.Api.Data.Sdk.Configuration
{
    public class JustGivingDataSdkConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("CharityId")]
        public int CharityId
        {
            get { return (int)this["CharityId"]; }
            set { this["CharityId"] = value; }
        }

        [ConfigurationProperty("ApiKey", IsRequired = false)]
        public string ApiKey
        {
            get
            {
                return (string)this["ApiKey"];
            }
            set
            {
                this["ApiKey"] = value;
            }
        }

        [ConfigurationProperty("RootDomain", IsRequired = false)]
        public string RootDomain
        {
            get
            {
                return (string)this["RootDomain"];
            }
            set
            {
                this["RootDomain"] = value;
            }
        }

        [ConfigurationProperty("ApiVersion", IsRequired = false)]
        public int ApiVersion
        {
            get
            {
                return (int)this["ApiVersion"];
            }
            set
            {
                this["ApiVersion"] = value;
            }
        }

        [ConfigurationProperty("Username")]
        public string Username
        {
            get { return (string)this["Username"]; }
            set { this["Username"] = value; }
        }

        [ConfigurationProperty("Password")]
        public string Password
        {
            get { return (string) this["Password"]; }
            set { this["Password"] = value; }

        }
    }
}