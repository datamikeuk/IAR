using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAR.Data;
using IAR.Models;
using System.Runtime.Versioning;
using System.DirectoryServices;
using System.Collections;

namespace IAR.Pages
{
    public class HomeModel : PageModel
    {
        private readonly RegisterContext _context;
        private readonly UserResolver _userResolver;

        public HomeModel(RegisterContext context, UserResolver userResolver)
        {
            _context = context;
            _userResolver = userResolver;
        }

        public User User { get; set; } = default!;

        [SupportedOSPlatform("windows")]
        public void OnGet()
        {
            // Console.WriteLine(_userResolver.GetIdentityName());
            // _userResolver.GetAllUsers();
            // Console.WriteLine(_userResolver.GetDisplayName());
        }

        public JsonResult OnGetAssetList()
        {
            var assetList = _context.Assets
                .Select(a => new { a.ID, a.Name }).ToList();
            return new JsonResult(assetList);
        }
    }
}