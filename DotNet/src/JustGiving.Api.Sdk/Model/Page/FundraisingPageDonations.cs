using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustGiving.Api.Sdk.Model.Page
{
    [DataContract(Name = "fundraisingPage", Namespace = "")]
    public class FundraisingPageDonations
    {
        [DataMember(Name = "donations", EmitDefaultValue = false)]
        public List<FundraisingPageDonation> Donations { get; set; }

        [DataMember(Name = "pagination", EmitDefaultValue = false)]
        public Pagination Pagination { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string PageShortUrl { get; set; }
    }
}
