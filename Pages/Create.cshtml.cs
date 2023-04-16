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
    [AllowAnonymous]
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

        public IList<String> Roles { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorized = User.IsInRole(Constants.AssetOwnersRole) ||
                            User.IsInRole(Constants.AssetAdministratorsRole);

            Console.WriteLine(User.Identity.Name);
            
            var currentUserId = UserManager.GetUserId(User);
            Console.WriteLine("Current user ID: " + currentUserId);

            if (!isAuthorized)
            {
                return Forbid();
            }

            if (currentUserId == null) {
                return Forbid();
            }

            Roles = new List<string>();
            var user = await UserManager.FindByIdAsync(currentUserId);
            if (user != null) {
                var roles = await UserManager.GetRolesAsync(user);
                Roles = roles;
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
