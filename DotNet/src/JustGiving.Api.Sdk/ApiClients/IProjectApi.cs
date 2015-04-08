using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustGiving.Api.Sdk.ApiClients
{
    public interface IProjectApi : IProjectApiAsync
    {
        ProjectApi.GlobalProject GlobalProjectById(int projectId);
    }
}
