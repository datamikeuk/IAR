using System.ComponentModel.DataAnnotations;

namespace IAR.Models
{
    public class Asset
    {
        public int ID { get; set; }

        [Display(Name = "Asset Name")]
        public required string Name { get; set; }

        [Display(Name = "Used For")]
        public string? UsedFor { get; set; }

        [Display(Name = "Executive Sponsor")]
        public string? ExecutiveSponsorAccountName { get; set; }

        [Display(Name = "Data Owner")]
        public string? DataOwnerAccountName { get; set; }

        [Display(Name = "Data Steward")]
        public string? DataStewardAccountName { get; set; }

        public int? BackEndPlatformID { get; set; }

        public int? FrontEndPlatformID { get; set; }

        // ----- Audit fields -----
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }

        [Display(Name = "Updated By")]
        public string? UpdatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }


        // ----- Navigation properties -----

        // Asset has a one-to-zero-or-many relationship with the ThirdParty table
        public ICollection<ThirdParty>? ThirdParties { get; set; }

        // Asset (BackEndPlatformID) has a one-to-zero-or-one relationship 
        // with the BackEndPlatform table
        [Display(Name = "Back-End Platform")]
        public BackEndPlatform? BackEndPlatform { get; set; }

        // Asset (FrontEndPlatformID) has a one-to-zero-or-one relationship 
        // with the BackEndPlatform table
        [Display(Name = "Front-End Platform")]

        public FrontEndPlatform? FrontEndPlatform { get; set; }

        // Asset (ExecutiveSponsorAccountName) has a one-to-many relationship 
        // with the User table
        public User? ExecutiveSponsor { get; set; }

        // Asset (DataOwnerAccountName) has a one-to-many relationship 
        // with the User table
        public User? DataOwner { get; set; }

        // Asset (DataStewardAccountName) has a one-to-many relationship 
        // with the User table
        public User? DataSteward { get; set; }
		}
}