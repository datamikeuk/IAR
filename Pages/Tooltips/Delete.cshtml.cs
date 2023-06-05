using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;

namespace IAR.Pages.Tooltips
{
    public class DeleteModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public DeleteModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Tooltip Tooltip { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Tooltips == null)
            {
                return NotFound();
            }

            var tooltip = await _context.Tooltips.FirstOrDefaultAsync(m => m.FieldName == id);

            if (tooltip == null)
            {
                return NotFound();
            }
            else 
            {
                Tooltip = tooltip;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Tooltips == null)
            {
                return NotFound();
            }
            var tooltip = await _context.Tooltips.FindAsync(id);

            if (tooltip != null)
            {
                Tooltip = tooltip;
                _context.Tooltips.Remove(Tooltip);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
