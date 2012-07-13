using System;
using System.Configuration;
using System.Linq.Expressions;

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
        [ConfigurationProperty("charityUserUserName", IsRequired = false)]
        public string CharityUserUserName
        {
            get { return (string)this["charityUserUserName"]; }
            set { this["charityUserUserName"] = value; }
        }
    }

    public class TestConfigurationsHelper
    {
        public static string GetProperty(Expression<Func<ITestConfigurations, string>> func)
        {
            var memberExpression = (MemberExpression)func.Body;
            var propertyName = memberExpression.Member.Name;

            return GetProperty(propertyName);
        }

        private static string GetProperty(string propertyToFind)
        {
            var configuration = (ITestConfigurations) ConfigurationManager.GetSection("testConfigurations");
            foreach (var property in configuration.GetType().GetProperties())
            {
                if (property.Name == propertyToFind)
                {
                    var value = property.GetValue(configuration, null);
                    return (string)value;
                }
            }
            
            return string.Empty;
        }
    }
}