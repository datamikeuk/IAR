using System.ComponentModel.DataAnnotations;
using Audit.EntityFramework;

namespace IAR.Models
{
    public class Asset
    {
        public int ID { get; set; }

        public bool Active { get; set; } = true;

        [Display(Name = "Asset Name")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public string? ExecutiveSponsorAccountName { get; set; }

        public string? DataOwnerAccountName { get; set; }
        
        public string? DataStewardAccountName { get; set; }

        // ----- Review fields -----

        [Display(Name = "Last Review Date")]
        public DateTime? LastReviewDate { get; set; }

        [Display(Name = "Reviewed By")]
        public string? ReviewedBy { get; set; }

        [Display(Name = "Review Cycle Months")]
        public byte ReviewCycleMonths { get; set; } = 12;

        [Display(Name = "Next Review Date")]
        public DateTime? NextReviewDate { get; set; }

        // ----- Asset detail fields -----

        public int? BackEndPlatformID { get; set; }

        public string? BackEndPlatformLocation { get; set; }

        public int? FrontEndPlatformID { get; set; }

        public string? FrontEndPlatformLocation { get; set; }

        [Display(Name = "Physical Location")]
        public PhysicalLocationEnum? PhysicalLocation { get; set; }

        public VolumeEnum? Volume { get; set; }

        [Display(Name = "Personal Data")]
        public string? PersonalData { get; set; }

        [Display(Name = "Accessed By")]
        public string? AccessedBy { get; set; }

        public string? Restricted { get; set; }

        [Display(Name = "Maintained By")]
        public string? MaintainedBy { get; set; }

        public string? Provider { get; set; }

        // ----- Audit fields -----

        [AuditIgnore]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }

        [Display(Name = "Updated By")]
        public string? UpdatedBy { get; set; }

        [AuditIgnore]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        // ----- Lawful Basis fields -----

        [Display(Name = "Lawful Basis Consent")]
        public bool LawfulBasisConsent { get; set; }

        [Display(Name = "Lawful Basis Consent Detail")]
        public string? LawfulBasisConsentDetail { get; set; }

        [Display(Name = "Lawful Basis Contract")]
        public bool LawfulBasisContract { get; set; }

        [Display(Name = "Lawful Basis Contract Detail")]
        public string? LawfulBasisContractDetail { get; set; }

        [Display(Name = "Lawful Basis Legal Obligation")]
        public bool LawfulBasisLegalObligation { get; set; }

        [Display(Name = "Lawful Basis Legal Obligation Detail")]
        public string? LawfulBasisLegalObligationDetail { get; set; }

        [Display(Name = "Lawful Basis Vital Interest")]
        public bool LawfulBasisVitalInterest { get; set; }

        [Display(Name = "Lawful Basis Vital Interest Detail")]
        public string? LawfulBasisVitalInterestDetail { get; set; }

        [Display(Name = "Lawful Basis Legitimate Interest")]
        public bool LawfulBasisLegitimateInterest { get; set; }

        [Display(Name = "Lawful Basis Legitimate Interest Detail")]
        public string? LawfulBasisLegitimateInterestDetail { get; set; }

        // ----- Special Category fields -----

        [Display(Name = "Special Racial/Ethnic")]
        public bool SpecialRacialEthnic { get; set; }

        [Display(Name = "Special Political Opinion")]
        public bool SpecialPoliticalOpinion { get; set; }

        [Display(Name = "Special Religious/Philosophical")]
        public bool SpecialReligiousPhilosophical { get; set; }

        [Display(Name = "Special Trade Union")]
        public bool SpecialTradeUnion { get; set; }

        [Display(Name = "Special Genetic")]
        public bool SpecialGenetic { get; set; }

        [Display(Name = "Special Biometric")]
        public bool SpecialBiometric { get; set; }

        [Display(Name = "Special Health")]
        public bool SpecialHealth { get; set; }

        [Display(Name = "Special Sexual")]
        public bool SpecialSexual { get; set; }

        [Display(Name = "Criminal Conviction")]
        public bool CriminalConviction { get; set; }

        [Display(Name = "Children")]
        public bool Children { get; set; }

        // ----- DPA fields -----

        [Display(Name = "Subject to DPA")]
        public bool SubjectToDPA { get; set; }

        [Display(Name = "Data Subjects")]
        public string? DataSubjects { get; set; }

        [Display(Name = "Secondary Purpose")]
        public bool SecondaryPurpose { get; set; }

        [Display(Name = "Secondary Purpose Details")]
        public string? SecondaryPurposeDetails { get; set; }

        [Display(Name = "Personal Details")]
        public bool PersonalDetails { get; set; }

        [Display(Name = "Goods and/or Services")]
        public bool GoodsServices { get; set; }

        [Display(Name = "Supplier Details")]
        public bool SupplierDetails { get; set; }

        [Display(Name = "Financial Details")]
        public bool FinancialDetails { get; set; }

        [Display(Name = "Lifestyle and/or Social")]
        public bool LifestyleSocial { get; set; }

        [Display(Name = "Complaints")]
        public bool Complaints { get; set; }

        [Display(Name = "Education and/or Employment")]
        public bool EducationEmployment { get; set; }

        [Display(Name = "Health and Safety and/or Security")]
        public bool HealthSafetySecurity { get; set; }

        [Display(Name = "Visual Images")]
        public bool VisualImages { get; set; } = false;

        // ----- Navigation properties -----
        [Display(Name = "Executive Sponsor")]
        public User? ExecutiveSponsor { get; set; }

        [Display(Name = "Data Owner")]
        public User? DataOwner { get; set; }

        [Display(Name = "Data Steward")]
        public User? DataSteward { get; set; }

        [Display(Name = "Back-End Platform")]
        public BackEndPlatform? BackEndPlatform { get; set; }

        [Display(Name = "Front-End Platform")]

        public FrontEndPlatform? FrontEndPlatform { get; set; }

        public ICollection<ThirdParty>? ThirdParties { get; set; }

        public ICollection<Note>? Notes { get; set; }

        public ICollection<RetentionPeriod>? RetentionPeriods { get; set; }
	}

    // ----- Enums -----
    public enum VolumeEnum
    {
        [Display(Name = "Very Small < 1,000 Records")]
        VerySmall,
        [Display(Name = "Small < 10,000 Records")]
        Small,
        [Display(Name = "Medium < 50,000 Records")]     
        Medium,
        [Display(Name = "Large < 100,000 Records")]
        Large,
        [Display(Name = "Very Large 100,000+ Records")]
        VeryLarge
    }

    public enum PhysicalLocationEnum
    {
        [Display(Name = "On-Site")]
        OnSite,
        [Display(Name = "Off-Site")]
        OffSite,
        [Display(Name = "Physical Store")]
        PhysicalStore
    }
}