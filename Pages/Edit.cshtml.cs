using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;
using Microsoft.AspNetCore.Authorization;

namespace IAR.Pages
{
    public class EditModel : PageModel
    {
        private readonly RegisterContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserResolver _userResolver;

        public EditModel(RegisterContext context, IAuthorizationService authorizationService, UserResolver userResolver)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userResolver = userResolver;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;
        public Tooltip Tooltip { get; set; } = default!;
        public SelectList BackEndPlatformNameSL { get; set; } = null!;
        public SelectList FrontEndPlatformNameSL { get; set; } = null!;
        public bool CanEdit { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id, string? partialName)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var assetToEdit =  await _context.Assets
                .Include(a => a.BackEndPlatform)
                .Include(a => a.FrontEndPlatform)
                .Include(a => a.ThirdParties)
                .Include(a => a.RetentionPeriods)
                .Include(a => a.Notes)
                .Include(a => a.ExecutiveSponsor)
                .Include(a => a.DataOwner)
                .Include(a => a.DataSteward)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (assetToEdit == null) { return NotFound(); }

            Asset = assetToEdit;

            var authResult = await _authorizationService.AuthorizeAsync(User, Asset, "Responsible");
            CanEdit = authResult.Succeeded;

            if (partialName == "ThirdParty" && Asset.ThirdParties != null)
            {
                return Partial("_ThirdPartyTable", Asset.ThirdParties);
            }

            if (partialName == "Retention" && Asset.RetentionPeriods != null)
            {
                return Partial("_RetentionPeriodTable", Asset.RetentionPeriods);
            }

            if (partialName == "Note" && Asset.RetentionPeriods != null)
            {
                return Partial("_NoteTable", Asset.Notes);
            }

            PopulateBackEndPlatformsDropDownList(_context, Asset.BackEndPlatformID);
            PopulateFrontEndPlatformsDropDownList(_context, Asset.FrontEndPlatformID);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetToUpdate = await _context.Assets
                .Include(a => a.ThirdParties)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (assetToUpdate == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, assetToUpdate, "Responsible");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Asset>(
                 assetToUpdate,
                 "asset",
                a => a.ID, 
                a => a.Name,
                a => a.Description,
                a => a.LastReviewDate,
                a => a.NextReviewDate,
                a => a.ExecutiveSponsorAccountName,
                a => a.DataOwnerAccountName,
                a => a.DataStewardAccountName,
                a => a.PhysicalLocation,
                a => a.Volume,
                a => a.BackEndPlatformID, 
                a => a.FrontEndPlatformID,
                a => a.AccessedBy,
                a => a.Restricted,
                a => a.Provider,
                a => a.MaintainedBy,
                a => a.SecondaryPurpose,
                a => a.SecondaryPurposeDetails,
                a => a.SubjectToDPA,
                a => a.PersonalDetails,
                a => a.GoodsServices,
                a => a.SupplierDetails,
                a => a.FinancialDetails,
                a => a.LifestyleSocial,
                a => a.Complaints,
                a => a.EducationEmployment,
                a => a.HealthSafetySecurity,
                a => a.VisualImages,
                a => a.DataSubjects,
                a => a.LawfulBasisConsent,
                a => a.LawfulBasisConsentDetail,
                a => a.LawfulBasisContract,
                a => a.LawfulBasisContractDetail,
                a => a.LawfulBasisLegalObligation,
                a => a.LawfulBasisLegalObligationDetail,
                a => a.LawfulBasisVitalInterest,
                a => a.LawfulBasisVitalInterestDetail,
                a => a.SpecialRacialEthnic,
                a => a.SpecialPoliticalOpinion,
                a => a.SpecialReligiousPhilosophical,
                a => a.SpecialTradeUnion,
                a => a.SpecialGenetic,
                a => a.SpecialBiometric,
                a => a.SpecialHealth,
                a => a.SpecialSexual,
                a => a.CriminalConviction,
                a => a.Children
            ))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./View");
            }

            PopulateBackEndPlatformsDropDownList(_context, assetToUpdate.BackEndPlatformID);
            PopulateFrontEndPlatformsDropDownList(_context, assetToUpdate.FrontEndPlatformID);
            return Page();
        }

        public void PopulateBackEndPlatformsDropDownList(RegisterContext _context, 
            object? selectedBackEndPlatform = null)
        {
            var BackEndPlatformsQuery = from p in _context.BackEndPlatforms
                                   orderby p.Name
                                   select p;
                BackEndPlatformNameSL = new SelectList(BackEndPlatformsQuery.AsNoTracking(),
                    nameof(BackEndPlatform.ID),
                    nameof(BackEndPlatform.Name),
                    selectedBackEndPlatform);
        }

        public void PopulateFrontEndPlatformsDropDownList(RegisterContext _context, 
            object? selectedFrontEndPlatform = null)
        {
            var FrontEndPlatformsQuery = from p in _context.FrontEndPlatforms
                                   orderby p.Name
                                   select p;
            FrontEndPlatformNameSL = new SelectList(FrontEndPlatformsQuery.AsNoTracking(),
                nameof(FrontEndPlatform.ID),
                nameof(FrontEndPlatform.Name),
                selectedFrontEndPlatform);
        }

        // Lookup tooltip text for a given field name
        public string GetTooltipText(string fieldName)
        {
            var tooltipText = "No entry in Tooltips table for this field: " + fieldName;
            Tooltip = _context.Tooltips.FirstOrDefault(t => t.FieldName == fieldName) ?? new Tooltip();
            if (Tooltip != null && Tooltip.TooltipText != null) {
                tooltipText = Tooltip.TooltipText;
            }
            return tooltipText;
        }

        public PartialViewResult OnGetThirdPartyModal(int id)
        {   
                var emptyThirdParty = new ThirdParty{ThirdPartyName="", AssetID=id};
                return Partial("_ThirdPartyModal", emptyThirdParty);
        }
        
        public async Task<IActionResult> OnPostThirdPartyModal(ThirdParty thirdPartyData)
        {
            var newThirdParty = new ThirdParty {
                ThirdPartyName = thirdPartyData.ThirdPartyName,
                Use = thirdPartyData.Use,
                AssetID = thirdPartyData.AssetID
            };

            ModelState.Remove("Name");

            if (await TryUpdateModelAsync<ThirdParty>(
                    newThirdParty,
                    "tp",
                    t => t.ThirdPartyName, t => t.Use, t => t.AssetID))
                {
                    _context.ThirdParties.Add(newThirdParty);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                return Partial("_ThirdPartyModal", thirdPartyData);
        }

        public PartialViewResult OnGetRetentionPeriodModal(int id)
        {   
                var emptyRetentionPeriod = new RetentionPeriod{RetainedData="", RetentionPeriodMonths=12, AssetID=id};
                return Partial("_RetentionPeriodModal", emptyRetentionPeriod);
        }

        public async Task<IActionResult> OnPostRetentionPeriodModal(RetentionPeriod retentionPeriodData)
        {
            var newRetentionPeriod = new RetentionPeriod {
                RetainedData = retentionPeriodData.RetainedData,
                RetentionPeriodMonths = retentionPeriodData.RetentionPeriodMonths,
                AssetID = retentionPeriodData.AssetID
            };

            ModelState.Remove("Name");

            if (await TryUpdateModelAsync<RetentionPeriod>(
                    newRetentionPeriod,
                    "rp",
                    r => r.RetainedData, r => r.RetentionPeriodMonths, r => r.AssetID))
                {
                    _context.RetentionPeriods.Add(newRetentionPeriod);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                return Partial("_RetentionPeriodModal", retentionPeriodData);
        }

        public PartialViewResult OnGetNoteModal(int id)
        {   
                var emptyNote = new Note{NoteText="", AssetID=id};
                return Partial("_NoteModal", emptyNote);
        }

        public async Task<IActionResult> OnPostNoteModal(Note noteData)
        {
            var newNote = new Note {
                NoteText = noteData.NoteText,
                AssetID = noteData.AssetID
            };

            ModelState.Remove("Name");

            if (await TryUpdateModelAsync<Note>(
                    newNote,
                    "n",
                    n => n.NoteText, n => n.AssetID))
                {
                    _context.Notes.Add(newNote);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                return Partial("_NoteModal", noteData);
        }

        public async Task<IActionResult> OnPostReviewAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetToUpdate = await _context.Assets
                .FirstOrDefaultAsync(a => a.ID == id);

            if (assetToUpdate == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, assetToUpdate, "Responsible");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            assetToUpdate.LastReviewDate = DateTime.Now;
            assetToUpdate.NextReviewDate = DateTime.Now.AddMonths(assetToUpdate.ReviewCycleMonths);
            assetToUpdate.ReviewedBy = _userResolver.GetAccountName();

            await _context.SaveChangesAsync();
            return RedirectToPage("./View");
        }
    }
}
