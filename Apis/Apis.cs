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
                string? accountname,
                bool? review
            ) => await db.Assets
                // .Include(a => a.BackEndPlatform)
                // .Include(a => a.FrontEndPlatform)
                // .Include(a => a.ThirdParties)
                .Where(a => a.Active)
                // if review is null show all assets, if true show assets that need review, if false show assets that don't need review
                .Where(a => review == null || (review == true && (a.NextReviewDate < DateTime.Now || a.NextReviewDate == null)) || (review == false && a.NextReviewDate > DateTime.Now))

                .Where(a => accountname == null 
                    || (accountname == "missing" && a.DataOwnerAccountName == null) 
                    || (a.ExecutiveSponsorAccountName != null ? a.ExecutiveSponsorAccountName : "") == accountname
                    || (a.DataOwnerAccountName != null ? a.DataOwnerAccountName : "") == accountname
                    || (a.DataStewardAccountName != null ? a.DataStewardAccountName : "") == accountname
                )
                .Select(a => new { 
                    a.ID,
                    a.Name,
                    a.ExecutiveSponsorAccountName,
                    a.DataOwnerAccountName,
                    a.DataStewardAccountName,
                    ExecutiveSponsorName = a.ExecutiveSponsor != null ? a.ExecutiveSponsor.DisplayName : "",
                    DataOwnerName = a.DataOwner != null ? a.DataOwner.DisplayName : "",
                    DataStewardName = a.DataSteward != null ? a.DataSteward.DisplayName : "",
                    LastReviewDate = a.LastReviewDate,
                    NextReviewDate = a.NextReviewDate,
                    // BackEndPlatformName = a.BackEndPlatform != null ? a.BackEndPlatform.Name : "",
                    // FrontEndPlatformName  = a.FrontEndPlatform != null ? a.FrontEndPlatform.Name : "",
                    // a.ThirdParties,
                    a.CreatedDate,
                    a.CreatedBy,
                    a.UpdatedDate,
                    a.UpdatedBy
                })
                .ToListAsync()
            );

            // app.MapPut("/api/review/{id}", async (RegisterContext db, int id) =>
            // {                
            //     var asset = await db.Assets.FindAsync(id);
            //     if (asset != null)
            //     {
            //         asset.LastReviewDate = DateTime.Now;
            //         asset.NextReviewDate = DateTime.Now.AddMonths(asset.ReviewCycleMonths);
            //         await db.SaveChangesAsync();
            //     }
            // });
        }
    }
}