using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Http
{
    [CollectionDataContract(Namespace = "", Name = "errors")]
    public class Errors : List<ErrorResponse>
    {
    }
}
