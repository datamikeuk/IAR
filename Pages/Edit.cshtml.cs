using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;
using IAR.Models.RegisterViewModels;

namespace IAR.Pages
{
    public class EditModel : EditPageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public EditModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;

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
                .FirstOrDefaultAsync(m => m.ID == id);

            if (assetToEdit == null)
            {
                return NotFound();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" && assetToEdit.ThirdParties != null)
            {
                // Pass IEnumerable<ThirdParty> to PartialView
                return Partial("_ThirdPartyTable", assetToEdit.ThirdParties);
            }
            Asset = assetToEdit;
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
                a => a.ID, a => a.DataOwner, a => a.BackEndPlatformID, a => a.FrontEndPlatformID, a => a.Name))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Table");
            }

            PopulateBackEndPlatformsDropDownList(_context, assetToUpdate.BackEndPlatformID);
            PopulateFrontEndPlatformsDropDownList(_context, assetToUpdate.FrontEndPlatformID);
            return Page();
        }
        public PartialViewResult OnGetThirdPartyModal(int id)
        {   
                var emptyThirdParty = new ThirdParty{Name="", AssetID=id};
                return Partial("_ThirdPartyModal", emptyThirdParty);
        }
        public async Task<IActionResult> OnPostThirdPartyModal(ThirdParty thirdPartyData)
        {
            var newThirdParty = new ThirdParty {
                Name = thirdPartyData.Name,
                Use = thirdPartyData.Use,
                AssetID = thirdPartyData.AssetID
            };

            if (await TryUpdateModelAsync<ThirdParty>(
                    newThirdParty,
                    "thirdparty",
                    t => t.ID, t => t.Name, t => t.Use, t => t.AssetID))
                {
                    _context.ThirdParties.Add(newThirdParty);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                return Partial("_AddThirdPartyModal", thirdPartyData);
        }
    }
}
