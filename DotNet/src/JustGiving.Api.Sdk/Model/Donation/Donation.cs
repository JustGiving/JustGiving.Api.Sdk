using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Donation
{
    [DataContract(Name = "donation", Namespace = "")]
    public class Donation
    {
        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        [DataMember(Name = "id")]
        public int DonationId { get; set; }

        [DataMember(Name = "image", EmitDefaultValue = false, IsRequired = false)]
        public string Image { get; set; }

        [DataMember(Name = "hasImage", EmitDefaultValue = false, IsRequired = false)]
        public bool HasImage { get; set; }

        [DataMember(Name = "donationDate", EmitDefaultValue = false, IsRequired = false)]
        public DateTime? DonationDate { get; set; }

        [DataMember(Name = "donationRef", EmitDefaultValue = false, IsRequired = false)]
        public string DonationRef { get; set; }

        [DataMember(Name = "donorDisplayName", EmitDefaultValue = false, IsRequired = false)]
        public string DonorDisplayName { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false, IsRequired = false)]
        public string Message { get; set; }
        
        [DataMember(Name = "estimatedTaxReclaim", EmitDefaultValue = false, IsRequired = false)]
        public decimal? EstimatedTaxReclaim { get; set; }
		
        [DataMember(Name = "source", EmitDefaultValue = false, IsRequired = false)]
        public string Source { get; set; }

        [DataMember(Name = "thirdPartyReference", EmitDefaultValue = false, IsRequired = false)]
        public string ThirdPartyReference { get; set; }

        public Donation()
        {
            DonorDisplayName = "Anonymous";
        }
    }
}
