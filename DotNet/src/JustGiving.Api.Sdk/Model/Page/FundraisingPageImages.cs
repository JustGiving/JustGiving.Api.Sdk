using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [CollectionDataContract(Name="images", ItemName = "image", Namespace="")]
    public class FundraisingPageImages : List<FundraisingPageImage>
    {
    }
}