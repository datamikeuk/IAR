using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Audit.EntityFramework;

namespace IAR.Models
{
    public class RetentionPeriod
    {
        public int ID { get; set; }
        
        public required int AssetID { get; set; }
        
        [Display(Name = "Retention Data/Policy")]
        public required string RetainedData { get; set; }

        [Display(Name = "Retention Period (Months)")]
        public required int RetentionPeriodMonths { get; set; } = 12;

        // Audit fields
        [JsonIgnore]
        [AuditIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        [AuditIgnore]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        [AuditIgnore]
        public DateTime UpdatedDate { get; set; }

        [JsonIgnore]
        [AuditIgnore]
        public string? UpdatedBy { get; set; }

        // Navigation property
        [JsonIgnore]
        public Asset? Asset { get; set; }
    }
}