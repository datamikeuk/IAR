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
        private readonly IHttpContextAccessor _httpContextAccessor;
    
        public ViewModel(RegisterContext context, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;
        public bool IsAdmin { get; set; }
        public string? AccountName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, "Admin");
            IsAdmin = authResult.Succeeded;
            AccountName = _httpContextAccessor.HttpContext?.User.FindFirst("AccountName")?.Value;
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