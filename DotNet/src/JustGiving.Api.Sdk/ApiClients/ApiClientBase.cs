namespace JustGiving.Api.Sdk.ApiClients
{
    public abstract class ApiClientBase
    {        
        protected readonly JustGivingClient Parent;

        protected ApiClientBase(JustGivingClient parent)
        {
            Parent = parent;
        }
    }
}