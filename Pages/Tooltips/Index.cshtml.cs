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
    public class IndexModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public IndexModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

        public IList<Tooltip> Tooltip { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tooltips != null)
            {
                Tooltip = await _context.Tooltips.ToListAsync();
            }
        }
    }
}
