using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc.RazorPages;
using IAR.Models;
// using IAR.Authorization;
// using Microsoft.AspNetCore.Identity;
using IAR.Pages;
using IAR.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IAR.Pages
{
    [AllowAnonymous]
    public class HomeModel : PageModel
    {
        private readonly RegisterContext _context;

        public HomeModel(RegisterContext context)
        {
            _context = context;
        }

        public JsonResult OnGetAssetList()
        {
            var assetList = _context.Assets
                .Select(a => new { a.ID, a.Name }).ToList();
            return new JsonResult(assetList);
        }
    }
}