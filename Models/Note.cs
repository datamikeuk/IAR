using System.Text.Json.Serialization;
using Audit.EntityFramework;

namespace IAR.Models
{
    public class Note
    {
        public int ID { get; set; }
        
        public required int AssetID { get; set; }
        
        public required string NoteText { get; set; }

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