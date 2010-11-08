namespace JustGiving.Api.Sdk.ApiClients
{
    public abstract class ApiClientBase
    {
        protected readonly JustGivingClientBase Parent;

        protected ApiClientBase(JustGivingClientBase parent)
        {
            Parent = parent;
        }
    }
}