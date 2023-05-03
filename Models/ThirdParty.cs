using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IAR.Models
{
    public class ThirdParty
    {
        public int ID { get; set; }

        public required int AssetID { get; set; }

        [Display(Name = "Third Party Name")]
        public required string ThirdPartyName { get; set; }

        [Required(ErrorMessage = "Use is required.")]
        public string? Use { get; set; }

        public LocationEnum? Location { get; set; }

        public string? LocationDetail { get; set; }


        // Audit fields
        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; } = default!;

        public DateTime UpdatedDate { get; set; }
        
        public string? UpdatedBy { get; set; } = default!;

        // Navigation property
        [JsonIgnore]
        public Asset? Asset { get; set; }
    }
    // Enums
    public enum LocationEnum
    {
        [Display(Name = "On Premise")]
        OnPremise,
        [Display(Name = "Off Site")]
        OffSite
    }
}