using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "branding", Namespace = "")]
    public class FundraisingPageBranding
    {
        [DataMember(Name = "buttonColour")]
        public string ButtonColour { get; set; }
        [DataMember(Name = "buttonTextColour")]
        public string ButtonTextColour { get; set; }
        [DataMember(Name = "headerTextColour")]
        public string HeaderTextColour { get; set; }
        [DataMember(Name = "thermometerBackgroundColour")]
        public string ThermometerBackgroundColour { get; set; }
        [DataMember(Name = "thermometerFillColour")]
        public string ThermometerFillColour { get; set; }
        [DataMember(Name = "thermometerTextColour")]
        public string ThermometerTextColour { get; set; }
    }
}