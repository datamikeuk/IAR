using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IAR.Data;
using IAR.Models;
using Microsoft.AspNetCore.Authorization;

namespace IAR.Pages
{
    public class ViewModel : PageModel
    {
        private readonly RegisterContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserResolver _userResolver;

        // userResolver is used to get the current user's account name
        public ViewModel(RegisterContext context, IAuthorizationService authorizationService, UserResolver userResolver)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userResolver = userResolver;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;
        public bool IsAdmin { get; set; }
        public string? AccountName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, "Admin");
            IsAdmin = authResult.Succeeded;
            AccountName = _userResolver.GetAccountName();
            return Page();
        }

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