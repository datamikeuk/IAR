using IAR.Models;

namespace IAR.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RegisterContext context)
        {
            if (context.Assets.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{AccountName="michaelj", DisplayName="Michael Jones"},
                new User{AccountName="m1kej", DisplayName="Mike Jones"}
            };

            context.Users.AddRange(users);

            var roles = new Role[]
            {
                new Role{AccountName="michaelj", RoleName="Admin"},
                new Role{AccountName="m1kej", RoleName="Admin"}
            };

            context.Roles.AddRange(roles);

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
                new Asset{Name="Test1", BackEndPlatformID=1, FrontEndPlatformID=1, 
                    DataOwner="Michael Jones", CreatedDate=currentDateTime, UpdatedDate=currentDateTime},
                new Asset{Name="Test2", BackEndPlatformID=2, FrontEndPlatformID=2, 
                    CreatedDate=currentDateTime, UpdatedDate=currentDateTime}
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