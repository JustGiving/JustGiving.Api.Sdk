using System;

namespace JustGiving.Api.Sdk.Model.Page
{
    public class AddFundraisingPageVideoRequest
    {
        public string Url { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public string PageShortName { get; set; }
    }
}