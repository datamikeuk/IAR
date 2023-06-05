using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Audit.EntityFramework;

namespace IAR.Models
{
    public class Tooltip
    {
        [Key]
        public string FieldName { get; set; } = default!;

        public string TooltipText { get; set; } = default!;

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
    }
}
