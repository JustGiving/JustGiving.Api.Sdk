namespace JustGiving.Api.Sdk.Test.Integration.Configuration
{
    public interface ITestConfigurations
    {
        string RflDomain { get; set; }
        int RflEventReference { get; set; }
        string RflUsernName { get; set; }
        int ValidEventId { get; set; }
        string ApiLocation { get; set; }
        string ApiKey { get; set; }
        string CharityUserUserName { get; set; }
        string CharityTestUserPassword { get; set; }
    }
}