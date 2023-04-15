namespace IAR.Models
{
    public class BackEndPlatform
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public ICollection<Asset> Assets { get; set; } = default!;
    }
}