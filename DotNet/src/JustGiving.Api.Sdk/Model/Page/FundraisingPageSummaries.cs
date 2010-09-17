using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [CollectionDataContract(Name = "fundraisingPages", ItemName = "fundraisingPage", Namespace = "")]
    public class FundraisingPageSummaries : List<FundraisingPageSummary>
    {
    }
}