using IAR.Data;
using IAR.Models;
using IAR.Models.RegisterViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IAR.Pages
{
    public class EditPageModel : PageModel
    {
        public SelectList BackEndPlatformNameSL { get; set; } = null!;
        public SelectList FrontEndPlatformNameSL { get; set; } = null!;

        public void PopulateBackEndPlatformsDropDownList(RegisterContext _context,
            object? selectedBackEndPlatform = null)
        {
            var BackEndPlatformsQuery = from p in _context.BackEndPlatforms
                                   orderby p.Name
                                   select p;
                BackEndPlatformNameSL = new SelectList(BackEndPlatformsQuery.AsNoTracking(),
                    nameof(BackEndPlatform.ID),
                    nameof(BackEndPlatform.Name),
                    selectedBackEndPlatform);
        }
        public void PopulateFrontEndPlatformsDropDownList(RegisterContext _context,
            object? selectedFrontEndPlatform = null)
        {
            var FrontEndPlatformsQuery = from p in _context.FrontEndPlatforms
                                   orderby p.Name
                                   select p;
            FrontEndPlatformNameSL = new SelectList(FrontEndPlatformsQuery.AsNoTracking(),
                nameof(FrontEndPlatform.ID),
                nameof(FrontEndPlatform.Name),
                selectedFrontEndPlatform);
        }
    }
}