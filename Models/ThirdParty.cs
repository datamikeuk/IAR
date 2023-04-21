using System.Text.Json.Serialization;

namespace IAR.Models
{
    public class ThirdParty
    {
        public int ID { get; set; }
        public required int AssetID { get; set; }
        public required string Name { get; set; }
        public string? Use { get; set; }
        public string? Location { get; set; }
        public string? GeoLocation { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; } = default!;

        // Navigation property
        [JsonIgnore]
        public Asset? Asset { get; set; }
    }
}