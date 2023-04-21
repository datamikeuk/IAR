using IAR.Data;
using Microsoft.EntityFrameworkCore;

namespace IAR.Apis
{
    public static partial class Apis
    {
        public static void GetApis(WebApplication app)
        {
            app.MapGet("/api/userlist", async (RegisterContext db, int listLength, string? name) =>
                await db.Users
                .Where(u => u.DisplayName != null && u.DisplayName.Contains(name??""))
                .Select(u => new { u.AccountName, u.DisplayName })
                .Take(listLength)
                .OrderBy(u => u.DisplayName)
                .ToListAsync()
            );

            app.MapGet("/api/assetlist", async (RegisterContext db) =>
                await db.Assets
                .Select(a => new { a.ID, a.Name })
                .ToListAsync()
            );

            app.MapGet("/api/tabledata", async (
                RegisterContext db,
                string? accountname
                ) => await db.Assets
                .Include(a => a.BackEndPlatform)
                .Include(a => a.FrontEndPlatform)
                .Include(a => a.ThirdParties)
                .Where(a => accountname == null 
                    || (accountname == "missing" && a.DataOwnerAccountName == null) 
                    || (a.DataOwnerAccountName != null ? a.DataOwnerAccountName : "") == accountname)
                .Select(a => new { 
                    a.ID,
                    a.Name,
                    ExecutiveSponsorName = a.ExecutiveSponsor != null ? a.ExecutiveSponsor.DisplayName : "",
                    DataOwnerName = a.DataOwner != null ? a.DataOwner.DisplayName : "",
                    DataStewardName = a.DataSteward != null ? a.DataSteward.DisplayName : "",
                    BackEndPlatformName = a.BackEndPlatform != null ? a.BackEndPlatform.Name : "",
                    FrontEndPlatformName  = a.FrontEndPlatform != null ? a.FrontEndPlatform.Name : "",
                    a.ThirdParties,
                    a.CreatedDate,
                    a.CreatedBy,
                    a.UpdatedDate,
                    a.UpdatedBy
                })
                .ToListAsync()
            );
        }
    }
}

