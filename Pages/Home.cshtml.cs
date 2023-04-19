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

        public JsonResult OnGetUserList()
        {
            var userList = _context.Users
                .Select(u => new { id = u.AccountName, text = u.DisplayName }).ToList();
                // .Select(u => new {u.AccountName, u.DisplayName }).ToList();
            return new JsonResult(userList);
        }
    }
}