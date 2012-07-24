using System;

namespace JustGiving.Api.Data.Sdk.Test.Integration.Configuration
{
    public interface ITestConfigurations
    {
        DateTime StartDate { get; set; }
        string ApiLocation { get; set; }
    }
}