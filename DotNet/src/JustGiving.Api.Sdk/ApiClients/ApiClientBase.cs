namespace JustGiving.Api.Sdk.ApiClients
{
    public abstract class ApiClientBase
    {
        public bool DebugEnabled { get; set; }
        protected readonly JustGivingClientBase Parent;

        protected ApiClientBase(JustGivingClientBase parent)
        {
            Parent = parent;
        }

        protected string ConfigureDebugging(string locationFormat)
        {
            if(!DebugEnabled) { return locationFormat; }

            locationFormat += !locationFormat.Contains("?") ? "?" : "&";
            locationFormat += "debug=true";
            return locationFormat;
        }
    }
}