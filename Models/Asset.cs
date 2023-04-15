using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAR.Models
{
    public class Asset
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? UsedFor { get; set; }
        public string? DataOwner { get; set; }
        // user ID from AspNetUser table.
        public string? OwnerID { get; set; }
        public int? BackEndPlatformID { get; set; }
        public BackEndPlatform? BackEndPlatform { get; set; }
        public int? FrontEndPlatformID { get; set; }
        public FrontEndPlatform? FrontEndPlatform { get; set; }
        public ICollection<ThirdParty>? ThirdParties { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
		}
}