namespace IAR.Models
{
    public class ThirdParty
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Use { get; set; }
        public string? Location { get; set; }
        public string? GeoLocation { get; set; }
        public required int AssetID { get; set; }
        public Asset? Asset { get; set; }
    }
}