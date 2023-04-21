using System.ComponentModel.DataAnnotations;

namespace IAR.Models
{
    public class BackEndPlatform
    {
        public int ID { get; set; }
        [Display(Name = "Back-End Platform")]
        public required string Name { get; set; }
        
        // Navigation properties
        public ICollection<Asset> Assets { get; set; } = default!;
    }
}