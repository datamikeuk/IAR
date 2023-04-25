using System.Text.Json.Serialization;

namespace IAR.Models
{
    public class RetentionPeriod
    {
        public int ID { get; set; }
        
        public required int AssetID { get; set; }
        
        public required string RetainedData { get; set; }

        public int? RetentionPeriodMonths { get; set; }

        // Audit fields

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; } = default!;

        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        public string UpdatedBy { get; set; } = default!;

        // Navigation property
        [JsonIgnore]
        public Asset? Asset { get; set; }
    }
}