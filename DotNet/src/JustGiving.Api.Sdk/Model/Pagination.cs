using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model
{
    [DataContract(Name = "pagination", Namespace = "")]
    public class Pagination
    {
        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [DataMember(Name = "totalPages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "totalResults")]
        public int TotalResults { get; set; }

        [DataMember(Name = "pageSizeRequested")]
        public int PageSizeRequested { get; set; }

        [DataMember(Name = "pageSizeReturned")]
        public int PageSizeReturned { get; set; }
    }
}