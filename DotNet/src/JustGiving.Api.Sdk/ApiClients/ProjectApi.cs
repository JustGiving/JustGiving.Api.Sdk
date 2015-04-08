using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
            get { return "{apiKey}/v{apiVersion}/project"; }
        }

        private string GlobalProjectResourcesEndpoint(int projectId)
        {
            return ResourceBase + "/global/" + projectId;
        }

        public GlobalProject GlobalProjectById(int projectId)
        {
            var resourceEndpoint = GlobalProjectResourcesEndpoint(projectId);
            var result = HttpChannel.PerformRequest<GlobalProject>("GET", resourceEndpoint);
            return result;
        }

        public void GlobalProjectByIdAsync(int projectId, Action<GlobalProject> callback)
        {
            var resourceEndpoint = GlobalProjectResourcesEndpoint(projectId);
            HttpChannel.PerformRequestAsync("GET", resourceEndpoint, callback);
        }

        [DataContract(Name = "globalProject", Namespace = "")]
        public class GlobalProject
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "need")]
            public string Need { get; set; }

            [DataMember(Name = "activities")]
            public string Activities { get; set; }

            [DataMember(Name = "impact")]
            public string Impact { get; set; }

            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "country")]
            public string Country { get; set; }

            [DataMember(Name = "region")]
            public string Region { get; set; }

            [DataMember(Name = "isActive")]
            public bool IsActive { get; set; }

            [DataMember(Name = "targetAmount")]
            public decimal TargetAmount { get; set; }

            [DataMember(Name = "totalRaised")]
            public decimal TotalRaised { get; set; }

            [DataMember(Name = "organisationName")]
            public string OrganisationName { get; set; }

            [DataMember(Name = "organisationCity")]
            public string OrganisationCity { get; set; }

            [DataMember(Name = "organisationCountry")]
            public string OrganisationCountry { get; set; }

            [DataMember(Name = "organisationUrl")]
            public string OrganisationUrl { get; set; }

            [DataMember(Name = "organisationLogoUrl")]
            public string OrganisationLogoUrl { get; set; }


            [DataMember(Name = "charityId")]
            public int CharityId { get; set; }

            [DataMember(Name = "projectId")]
            public int CharityRevenueStreamId { get; set; }


            [DataMember(Name = "donateUrl")]
            public string DonateUrl { get; set; }


            [DataMember(Name = "images")]
            public List<GlobalGivingImage> Images { get; set; }

            [DataMember(Name = "progressUpdates")]
            public List<ProgressUpdate> ProgressUpdates { get; set; }

            public GlobalProject()
            {
                Images = new List<GlobalGivingImage>();
                ProgressUpdates = new List<ProgressUpdate>();
            }
        }

        [DataContract(Name = "progressUpdate", Namespace = "")]
        public class ProgressUpdate
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "authorName")]
            public string AuthorName { get; set; }

            [DataMember(Name = "content")]
            public string Content { get; set; }

            [DataMember(Name = "datePublished")]
            public DateTime DatePublished { get; set; }
        }

        [DataContract(Name = "image", Namespace = "")]
        public class GlobalGivingImage
        {
            [DataMember(Name = "title")]
            public virtual string Title { get; set; }

            [DataMember(Name = "thumbnailUri")]
            public virtual string ThumbnailUri { get; set; }

            [DataMember(Name = "uri")]
            public virtual string Uri { get; set; }

        }
    }
}
