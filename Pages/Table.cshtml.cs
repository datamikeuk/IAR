using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Models;
using IAR.Models.ViewModels;

namespace IAR.Pages
{
    [AllowAnonymous]
    public class TableModel : PageModel
    {
        private readonly RegisterContext _context;

        public TableModel(RegisterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Asset Asset { get; set; } = default!;

        public JsonResult OnGetTableData(string? nameFilter, bool? editCheck)
        {   
            IQueryable<Asset> allAssets = _context.Assets
                .Include(a => a.BackEndPlatform)
                .Include(a => a.FrontEndPlatform)
                .Include(a => a.ThirdParties);                
            
            if (editCheck == true) { allAssets = allAssets.Where(a => a.DataOwner == "Michael Jones"); }
            if (nameFilter != null) { allAssets = allAssets.Where(a => a.Name.Contains(nameFilter)); }
            
            var tableData = new List<TableData>();

            foreach (var asset in allAssets) 
            {   
                var backEndPlatformName = "";
                if(asset.BackEndPlatform != null){ backEndPlatformName = asset.BackEndPlatform.Name; }
                var frontEndPlatformName = "";
                if(asset.FrontEndPlatform != null){ frontEndPlatformName = asset.FrontEndPlatform.Name; }
                var thirdPartyNames = "";
                if(asset.ThirdParties != null){ 
                    foreach (var thirdParty in asset.ThirdParties)
                    {
                        thirdPartyNames += thirdParty.Name + ",";
                    }
                }
                tableData.Add(new TableData()
                {
                    ID = asset.ID,
                    Name = asset.Name,
                    UsedFor = asset.UsedFor,
                    DataOwner = asset.DataOwner,
                    BackEndPlatformName = backEndPlatformName,
                    FrontEndPlatformName = frontEndPlatformName,
                    ThirdPartyNames = thirdPartyNames,
                    CreatedDate = asset.CreatedDate,
                    UpdatedDate = asset.UpdatedDate
                });
            }
            return new JsonResult(tableData);
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