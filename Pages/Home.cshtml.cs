using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc.RazorPages;
using IAR.Models;
using IAR.Authorization;
using Microsoft.AspNetCore.Identity;
// using IAR.Pages;
using IAR.Data;

namespace IAR.Pages
{
    [AllowAnonymous]
    public class HomeModel : DI_BasePageModel
    {
        private readonly RegisterContext _context;

        public HomeModel(RegisterContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, 
                                                         authorizationService, 
                                                         userManager)
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