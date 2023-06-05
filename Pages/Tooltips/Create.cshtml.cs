using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IAR.Data;
using IAR.Models;

namespace IAR.Pages.Tooltips
{
    public class CreateModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public CreateModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tooltip Tooltip { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Tooltips == null || Tooltip == null)
            {
                return Page();
            }

            _context.Tooltips.Add(Tooltip);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
