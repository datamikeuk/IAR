using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IAR.Data;
using IAR.Models;
using IAR.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IAR.Pages
{
    public class CreateModel : DI_BasePageModel
    {
        private readonly RegisterContext _context;

        public CreateModel(RegisterContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base(context, 
                                                         authorizationService, 
                                                         userManager)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var isAuthorized = User.IsInRole(Constants.AssetOwnersRole) ||
                            User.IsInRole(Constants.AssetAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized)
            {
                return Forbid();
            }

            ViewData["BackEndPlatformID"] = new SelectList(_context.BackEndPlatforms, "ID", "ID");
            ViewData["FrontEndPlatformID"] = new SelectList(_context.FrontEndPlatforms, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Assets == null || Asset == null)
            {
                return Page();
            }

            _context.Assets.Add(Asset);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
