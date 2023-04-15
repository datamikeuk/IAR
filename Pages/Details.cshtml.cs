using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;

namespace IAR.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public DetailsModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

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
    }
}