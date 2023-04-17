using Microsoft.AspNetCore.Mvc;
using IAR.Models;
using IAR.Pages;
using IAR.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace IAR.Pages
{
    [Authorize(Policy ="Admin")]
    public class HomeModel : PageModel
    {
        private readonly RegisterContext _context;

        public HomeModel(RegisterContext context)
        {
            _context = context;
        }

        // public void OnGet()
        // {
        //     if (User.Identity != null)
        //     {
        //         Console.WriteLine(User.Identity.Name);
        //     }
        // }

        public JsonResult OnGetAssetList()
        {
            var assetList = _context.Assets
                .Select(a => new { a.ID, a.Name }).ToList();
            return new JsonResult(assetList);
        }
    }
}