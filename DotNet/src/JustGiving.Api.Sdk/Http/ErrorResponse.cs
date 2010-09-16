using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Http
{
    [DataContract(Namespace = "", Name = "error")]
    public class ErrorResponse
    {
        [DataMember(Name = "id", IsRequired = true, Order = 0)]
        public string Id { get; set; }

        [DataMember(Name = "desc", IsRequired = true, Order = 1)]
        public string Description { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(string id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}