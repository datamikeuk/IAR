using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAR.Data;
using IAR.Models;
using Microsoft.EntityFrameworkCore;

namespace IAR.Pages
{
    public class ViewModel : PageModel
    {
        private readonly RegisterContext _context;

        public ViewModel(RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;

        public PartialViewResult OnGetAssetModal()
        {         
            return Partial("_AssetModal");
        }
        public async Task<IActionResult> OnPostAssetModal()
        {
            var emptyAsset = new Asset(){Name = ""};

            if (await TryUpdateModelAsync<Asset>(
                    emptyAsset,
                    "asset",
                    a => a.Name))
                {
                    _context.Assets.Add(emptyAsset);
                    await _context.SaveChangesAsync();

                    return new JsonResult(new { success = true , id = emptyAsset.ID});
                }
                return Partial("_AssetModal");
        }
    }
}