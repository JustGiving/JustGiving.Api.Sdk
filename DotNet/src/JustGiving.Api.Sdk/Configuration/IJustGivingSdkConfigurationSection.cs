namespace JustGiving.Api.Sdk.Configuration
{
    public interface IJustGivingSdkConfigurationSection
    {
        string ApiKey { get; set; }

        string RootDomain { get; set; }

        int ApiVersion { get; set; }

        WireDataFormat DefaultWireDataFormat { get; set; }

        bool DefaultZip { get; set; }

        string Username { get; set; }

        string Password { get; set; }

        int TimeOutSecs { get; set; }

        int? CharityId { get; set; }
    }
}