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
    public class ListModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public ListModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

        public IList<Asset> Asset { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Assets != null)
            {
                Asset = await _context.Assets
                .Include(a => a.BackEndPlatform)
                .Include(a => a.DataOwner)
                .Include(a => a.DataSteward)
                .Include(a => a.ExecutiveSponsor)
                .Include(a => a.FrontEndPlatform).ToListAsync();
            }
        }
    }
}
