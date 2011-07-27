using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "pageTheme")]
    public class PageTheme
    {
        [DataMember(Name = "backgroundColour")]
        public string BackgroundColour { get; set; }

        [DataMember(Name = "buttonColour")]
        public string ButtonColour { get; set; }

        [DataMember(Name = "buttonTextColour")]
        public string ButtonTextColour { get; set; }

        [DataMember(Name = "titleColour")]
        public string TitleColour { get; set; }
    }
}