namespace IAR.Models.ViewModels
{
    public class TableData
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? UsedFor { get; set; }
        public string? DataOwner { get; set; }
        public string? BackEndPlatformName { get; set; }
        public string? FrontEndPlatformName { get; set; }
        public string? ThirdPartyNames { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}