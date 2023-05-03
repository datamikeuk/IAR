using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;

namespace IAR.Pages
{
    public class EditModel : PageModel
    {
        private readonly RegisterContext _context;

        public EditModel(RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;
        public Tooltip Tooltip { get; set; } = default!;
        public SelectList BackEndPlatformNameSL { get; set; } = null!;
        public SelectList FrontEndPlatformNameSL { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var assetToEdit =  await _context.Assets
                .Include(a => a.BackEndPlatform)
                .Include(a => a.FrontEndPlatform)
                .Include(a => a.ThirdParties)
                .Include(a => a.ExecutiveSponsor)
                .Include(a => a.DataOwner)
                .Include(a => a.DataSteward)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (assetToEdit == null) { return NotFound(); }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" && assetToEdit.ThirdParties != null)
            {
                // Pass IEnumerable<ThirdParty> to PartialView
                return Partial("_ThirdPartyTable", assetToEdit.ThirdParties);
            }

            Asset = assetToEdit;

            // var isAuthorized = User.IsInRole(Constants.AssetOwnersRole) ||
            //             User.IsInRole(Constants.AssetAdministratorsRole);

            // var currentUserId = UserManager.GetUserId(User);

            // if (!isAuthorized && currentUserId != Asset.OwnerID)
            // {
            //     return Forbid();
            // }

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
                .FirstOrDefaultAsync(s => s.ID == id);

            if (assetToUpdate == null)
            {
                return NotFound();
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
                a => a.FrontEndPlatformID
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

        //Lookup tooltip text for a given field name
        public string GetTooltipText(string fieldName)
        {
            var tooltipText = "";
            Tooltip = _context.Tooltips.FirstOrDefault(t => t.FieldName == fieldName) ?? new Tooltip();
            if (Tooltip != null) {
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

            // ModelState.Remove("Name");

            if (await TryUpdateModelAsync<ThirdParty>(
                    newThirdParty,
                    "tp",
                    t => t.ID, t => t.ThirdPartyName, t => t.Use, t => t.AssetID))
                {
                    _context.ThirdParties.Add(newThirdParty);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                return Partial("_ThirdPartyModal", thirdPartyData);
        }
    }
}
