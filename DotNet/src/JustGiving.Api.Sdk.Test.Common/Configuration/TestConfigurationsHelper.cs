using System;
using System.Configuration;
using System.Linq.Expressions;

namespace JustGiving.Api.Sdk.Test.Common.Configuration
{
    public class TestConfigurationsHelper
    {
        public static TReturnType GetProperty<T, TReturnType>(Expression<Func<T, TReturnType>> func)
        {
            var memberExpression = (MemberExpression)func.Body;
            var propertyName = memberExpression.Member.Name;

            return GetProperty<T, TReturnType>(propertyName);
        }

        private static TReturnType GetProperty<T, TReturnType>(string propertyToFind)
        {
            var configuration = (T)ConfigurationManager.GetSection("testConfigurations");
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