using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;

namespace IAR.Pages.Tooltips
{
    public class EditModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public EditModel(IAR.Data.RegisterContext context)
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

            var tooltip =  await _context.Tooltips.FirstOrDefaultAsync(m => m.FieldName == id);
            if (tooltip == null)
            {
                return NotFound();
            }
            Tooltip = tooltip;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Tooltip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TooltipExists(Tooltip.FieldName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TooltipExists(string id)
        {
          return (_context.Tooltips?.Any(e => e.FieldName == id)).GetValueOrDefault();
        }
    }
}
