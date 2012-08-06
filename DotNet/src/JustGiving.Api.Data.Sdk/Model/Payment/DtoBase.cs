using System.Net;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk.Model.Payment
{
    [DataContract(Namespace = "", Name = "dtoBase")]
    public abstract class DtoBase
    {
        protected DtoBase()
        {
            HttpStatusCode = HttpStatusCode.OK;
            StatusSummary = "OK";
        }

        [DataMember]
        public virtual HttpStatusCode HttpStatusCode { get; set; }
        [DataMember]
        public virtual string StatusSummary { get; set; }

        public void SetStatusCode(HttpStatusCode statusCode, string statusSummary = "")
        {
            HttpStatusCode = statusCode;
            StatusSummary = statusSummary;
        }
    }
}