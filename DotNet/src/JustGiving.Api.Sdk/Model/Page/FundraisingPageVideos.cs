using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [CollectionDataContract(Name="videos", ItemName="video", Namespace="")]
    public class FundraisingPageVideos : List<FundraisingPageVideo>
    {
    }
}