using IAR.Models;

namespace IAR.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RegisterContext context)
        {
            // Look for any assets.
            if (context.Assets.Any())
            {
                return;   // DB has been seeded
            }

            // var tp1 = new ThirdParty
            // {
            //     AssetID = 1,
            //     Name = "ThirdParty1"
            // };
            // var tp2 = new ThirdParty
            // {
            //     AssetID = 1,
            //     Name = "ThirdParty2"
            // };

            // var thirdparties = new ThirdParty[]
            // {
            //     tp1,
            //     tp2
            // };

            // context.AddRange(thirdparties);

            // var a1 = new Asset
            // {
            //     Name = "Asset1",
            //     ThirdParties = new List<ThirdParty> {tp1,tp2}
            // };

            // var a2 = new Asset
            // {
            //     Name = "Asset2",
            //     ThirdParties = new List<ThirdParty> {tp2}
            // };

            // var assets = new Asset[]
            // {
            //     a1,
            //     a2
            // };

            // context.AddRange(assets);
            // context.SaveChanges();

            var users = new User[]
            {
                new User{Name="STAFFSHOUSING\\michaelj", Role="Admin"},
                new User{Name="Other"}
            };
            context.Users.AddRange(users);

            var backendplatforms = new BackEndPlatform[]
            {
                new BackEndPlatform{Name="BackEndPlatform1"},
                new BackEndPlatform{Name="BackEndPlatform2"}
            };

            context.BackEndPlatforms.AddRange(backendplatforms);

            var frontendplatforms = new FrontEndPlatform[]
            {
                new FrontEndPlatform{Name="FrontEndPlatform1"},
                new FrontEndPlatform{Name="FrontEndPlatform2"}
            };

            context.FrontEndPlatforms.AddRange(frontendplatforms);
            context.SaveChanges();

            var currentDateTime = DateTime.Now;

            var assets = new Asset[]
            {
                new Asset{Name="Test1", BackEndPlatformID=1, FrontEndPlatformID=1, DataOwner="Michael Jones", CreatedDate=currentDateTime, UpdatedDate=currentDateTime},
                new Asset{Name="Test2", BackEndPlatformID=2, FrontEndPlatformID=2, CreatedDate=currentDateTime, UpdatedDate=currentDateTime}
            };

            context.Assets.AddRange(assets);
            context.SaveChanges();

            var thirdparties = new ThirdParty[]
            {
                new ThirdParty{Name="ThirdParty1", Use="Test1", AssetID=1},
                new ThirdParty{Name="ThirdParty2", Use="Test2", AssetID=1},
                new ThirdParty{Name="ThirdParty3", Use="Test3", AssetID=2},
            };
            context.ThirdParties.AddRange(thirdparties);
            context.SaveChanges();
        }
    }
}