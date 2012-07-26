using System;
using System.Configuration;
using System.Linq.Expressions;

namespace JustGiving.Api.Data.Sdk.Configuration
{
    public class DataSdkConfigurationManager
    {
        public static TReturnType GetProperty<TReturnType>(Expression<Func<JustGivingDataSdkConfiguration, TReturnType>> func)
        {
            var memberExpression = (MemberExpression)func.Body;
            var propertyName = memberExpression.Member.Name;

            return GetProperty<TReturnType>(propertyName);
        }

        private static TReturnType GetProperty<TReturnType>(string propertyToFind)
        {
            var configuration = ConfigurationManager.GetSection("justGivingDataSdk");
            foreach (var property in configuration.GetType().GetProperties())
            {
                if (property.Name == propertyToFind)
                {
                    var value = property.GetValue(configuration, null);
                    return (TReturnType)value;
                }
            }

            return default(TReturnType);
        }
    }
}