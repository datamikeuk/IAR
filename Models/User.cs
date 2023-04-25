using Audit.EntityFramework;

namespace IAR.Models
{
    [AuditIgnore]
    public class User
    {
        public string? AccountName { get; set; }
        public string? DisplayName { get; set; }

        // Navigation properties
        public ICollection<Asset> ExecutiveSponsorAssets { get; set; } = default!;
        public ICollection<Asset> DataOwnerAssets { get; set; } = default!;
        public ICollection<Asset> DataStewardAssets { get; set; } = default!;
    }
}