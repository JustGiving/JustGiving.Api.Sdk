using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
	public abstract class ApiClientBase
	{
	    public readonly HttpChannel HttpChannel;

		public abstract string ResourceBase { get; }

		protected ApiClientBase(HttpChannel channel)
		{
			HttpChannel = channel;
		}
    }
}