using Audit.EntityFramework;

namespace IAR.Models
{
    [AuditIgnore]
    public class Role
    {
        public int ID { get; set; }
        public string? AccountName { get; set; }
        public string? RoleName { get; set; }
    }
}