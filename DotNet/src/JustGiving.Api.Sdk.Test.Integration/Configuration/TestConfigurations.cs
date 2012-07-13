using System.Configuration;

namespace JustGiving.Api.Sdk.Test.Integration.Configuration
{
    public class TestConfigurations : ConfigurationSection, ITestConfigurations
    {

        [ConfigurationProperty("rflDomain", IsRequired = false, DefaultValue = "www.local.raceforlifesponsorme.org")]
        public string RflDomain
        {
            get { return (string) this["rflDomain"]; }
            set { this["rflDomain"] = value; }  
        }

        public string CharityUserUserName
        {
            get { return (string)this["charityUserUserName"]; }
            set { this["charityUserUserName"] = value; }
        }
    }

    public interface ITestConfigurations
    {
        string RflDomain { get; set;}
        string CharityUserUserName { get; set; }
    }
}