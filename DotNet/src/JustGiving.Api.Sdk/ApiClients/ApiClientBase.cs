using System;

namespace JustGiving.Api.Sdk.ApiClients
{
	public abstract class ApiClientBase
	{
		protected readonly JustGivingClientBase Parent;

		public abstract string ResourceBase { get; }

		protected ApiClientBase(JustGivingClientBase parent)
		{
			Parent = parent;
		}

		public T Get<T>(string location)
		{
			return PerformRequest<T>("GET", location);
		}

		public void GetAsync<T>(string location, Action<T> callback)
		{
			PerformRequestAsync("GET", location, callback);
		}

		public TResponseType Put<TRequestType, TResponseType>(string location, TRequestType request) where TRequestType : class
		{
			return Parent.HttpChannel.PerformApiRequest<TRequestType, TResponseType>("PUT", ResourceBase, request);
		}

		public void PutAsync<TRequestType, TResponseType>(string location, TRequestType request, Action<TResponseType> callback) where TRequestType : class
		{
			Parent.HttpChannel.PerformApiRequestAsync("PUT", ResourceBase, request, callback);
		}

		public T PerformRequest<T>(string verb, string location)
		{
			return Parent.HttpChannel.PerformApiRequest<T>(verb, location);
		}

		public void PerformRequestAsync<T>(string verb, string location, Action<T> callback)
		{
			Parent.HttpChannel.PerformApiRequestAsync(verb, location, callback);
		}
    }
}