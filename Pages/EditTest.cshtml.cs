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

namespace IAR.Pages
{
    public class EditTestModel : PageModel
    {
        private readonly IAR.Data.RegisterContext _context;

        public EditTestModel(IAR.Data.RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;
        // public VolumeEnum VolumeEnum { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assets == null)
            {
                return NotFound();
            }

            var asset =  await _context.Assets.FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null)
            {
                return NotFound();
            }
            Asset = asset;
           ViewData["BackEndPlatformID"] = new SelectList(_context.BackEndPlatforms, "ID", "ID");
           ViewData["DataOwnerAccountName"] = new SelectList(_context.Users, "AccountName", "AccountName");
           ViewData["DataStewardAccountName"] = new SelectList(_context.Users, "AccountName", "AccountName");
           ViewData["ExecutiveSponsorAccountName"] = new SelectList(_context.Users, "AccountName", "AccountName");
           ViewData["FrontEndPlatformID"] = new SelectList(_context.FrontEndPlatforms, "ID", "ID");
        //    VolumeEnum = new VolumeEnum();
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

            _context.Attach(Asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(Asset.ID))
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

        private bool AssetExists(int id)
        {
          return (_context.Assets?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
