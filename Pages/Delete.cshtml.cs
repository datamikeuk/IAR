using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IAR.Models;
using IAR.Data;

namespace IAR.Pages
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly RegisterContext _context;

        public DeleteModel(RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FirstOrDefaultAsync(m => m.ID == id);

            if (asset == null)
            {
                return NotFound();
            }
            else 
            {
                Asset = asset;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }
            var asset = await _context.Assets.FindAsync(id);

            if (asset != null)
            {
                Asset = asset;
                _context.Assets.Remove(Asset);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("View");
        }
    }
}