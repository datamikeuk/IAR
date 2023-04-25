namespace IAR.Models
{
    // This class is used to audit the Asset table
    public class AuditLog
    {
        public int ID { get; set; }

        // public string? EntityType { get; set; }
        
        public string Table { get; set; } = default!;

        public int TableID { get; set; }

        public string Action { get; set; } = default!;

        public string AuditData { get; set; } = default!;

        public string Changes { get; set; } = default!;

        public DateTime Date { get; set; } = default!;

        public string User { get; set; } = default!;
    }
}