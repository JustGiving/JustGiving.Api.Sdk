using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk.ApiClients
{
    public class ProjectApi : ApiClientBase, IProjectApi
    {
        public ProjectApi(HttpChannel channel)
            : base(channel)
        {
        }

        public override string ResourceBase
        {
            get { throw new NotImplementedException(); }
        }
    }
}
