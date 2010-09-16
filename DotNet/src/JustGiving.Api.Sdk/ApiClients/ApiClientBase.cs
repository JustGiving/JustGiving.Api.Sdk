namespace JustGiving.Api.Sdk.ApiClients
{
    public class ApiClientBase
    {        
        protected readonly JustGivingClient Parent;
        public ApiClientBase(JustGivingClient parent)
        {
            Parent = parent;
        }
    }
}