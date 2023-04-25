using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;

namespace IAR.Models
{
    [AuditIgnore]
    public class FrontEndPlatform
    {
        public int ID { get; set; }
        [Display(Name = "Front-End Platform")]
        public required string Name { get; set; }

        // Navigation properties
        public ICollection<Asset> Assets { get; set; } = default!;
    }
}