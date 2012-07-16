using System.Configuration;

namespace JustGiving.Api.Sdk.Configuration
{
    public class JustGivingSdkConfigurationSection : ConfigurationSection, IJustGivingSdkConfigurationSection
    {
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

        [ConfigurationProperty("DefaultWireDataFormat", DefaultValue = WireDataFormat.Json, IsRequired = false)]
        public WireDataFormat DefaultWireDataFormat
        {
            get
            {
                return (WireDataFormat)this["DefaultWireDataFormat"];
            }
            set
            {
                this["DefaultWireDataFormat"] = value;
            }
        }

        [ConfigurationProperty("DefaultZip", DefaultValue = true, IsRequired = false)]
        public bool DefaultZip
        {
            get
            {
                return (bool)this["DefaultZip"];
            }
            set
            {
                this["DefaultZip"] = value;
            }
        }

        [ConfigurationProperty("Username", IsRequired = false)]
        public string Username
        {
            get
            {
                return (string)this["Username"];
            }
            set
            {
                this["Username"] = value;
            }
        }

        [ConfigurationProperty("Password", IsRequired = false)]
        public string Password
        {
            get
            {
                return (string)this["Password"];
            }
            set
            {
                this["Password"] = value;
            }
        }

        [ConfigurationProperty("TimeOutSecs", DefaultValue = 120, IsRequired = false)]
        public int TimeOutSecs
        {
            get
            {
                return (int)this["TimeOutSecs"];
            }
            set
            {
                this["TimeOutSecs"] = value;
            }
        }

        [ConfigurationProperty("CharityId", DefaultValue = null, IsRequired = false)]
        public int? CharityId
        {
            get
            {
                if (this["CharityId"] != null)
                {
                    return (int)this["CharityId"];
                }

                return null;
            }
            set
            {
                this["CharityId"] = value;
            }
        }
    }
}