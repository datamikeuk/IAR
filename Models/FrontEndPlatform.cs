namespace IAR.Models
{
    public class FrontEndPlatform
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public ICollection<Asset> Assets { get; set; } = default!;
    }
}