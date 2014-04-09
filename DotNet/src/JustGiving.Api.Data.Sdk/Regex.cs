using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustGiving.Api.Data.Sdk
{
    public static class Regex
    {
        public const string CustomCode = "^[a-zA-Z0-9]*$";
    }

    /// <summary>
    /// Implementation of a WebDAV multi-status response. For Restful handling of bulk operations where some parts can succeed and some fail.
    /// </summary>
    /// <see href="http://www.webdav.org/specs/rfc4918.html#multi-status.response"/>
    /// <seealso cref="MultiStatusResponse"/>
    /// <remarks>See http://www.webdav.org/specs/rfc4918.html#multi-status.response for more information about WebDAV multi-status responses.</remarks>
    [CollectionDataContract(Namespace = "DAV:", Name = "multistatus")]
    public class MultiStatus : List<MultiStatusResponse>
    {
        internal void Add(string href, int status)
        {
            Add(new MultiStatusResponse { Href = href, Status = status });
        }
    }

    /// <summary>
    /// Represents the a single result of a Http operation within a multi status response.
    /// </summary>
    /// <seealso cref="MultiStatus" />
    [DataContract(Namespace = "DAV:", Name = "response")]
    public class MultiStatusResponse
    {
        /// <summary>
        /// The location of a resource which was addressed in the request
        /// </summary>
        [DataMember(Name = "href")]
        public string Href { get; set; }

        /// <summary>
        /// The result of the action performed on the resource
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }
    }
}