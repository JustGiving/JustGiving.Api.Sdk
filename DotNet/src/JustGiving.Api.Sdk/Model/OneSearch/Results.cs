using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.OneSearch
{
    [DataContract(Name = "Results", Namespace = "")]
    public class Results
    {
        [DataMember(Name = "EventIds")]
        public int[] EventIds { get; set; }

        [DataMember(Name = "Subtext")]
        public string Subtext { get; set; }

        [DataMember(Name = "Link")]
        public string Link { get; set; }

        [DataMember(Name = "LinkPath")]
        public string LinkPath { get; set; }

        [DataMember(Name = "CountryCode")]
        public string CountryCode { get; set; }

        [DataMember(Name = "ProfileWhat")]
        public string ProfileWhat { get; set; }

        [DataMember(Name = "ProfileWhy")]
        public string ProfileWhy { get; set; }

        [DataMember(Name = "Highlight")]
        public string Highlight { get; set; }

        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "Logo")]
        public string Logo { get; set; }

        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "Score")]
        public double Score { get; set; }
    }
}
