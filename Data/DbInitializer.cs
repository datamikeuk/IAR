using IAR.Models;

namespace IAR.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RegisterContext context)
        {
            var currentDateTime = DateTime.Now;

            if (context.Assets.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{AccountName="michaelj", DisplayName="Michael Jones"},
                new User{AccountName="m1kej", DisplayName="Mike Jones"},
                new User{AccountName="test", DisplayName="Test Test"}
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

            var assets = new Asset[]
            {
                new Asset {
                    Name="Test1",
                    Description="Testing",
                    BackEndPlatformID=1,
                    FrontEndPlatformID=2, 
                    ExecutiveSponsorAccountName="m1kej",
                    DataOwnerAccountName="michaelj",
                    DataStewardAccountName="test",
                    CreatedDate=currentDateTime,
                    CreatedBy="m1kej",
                    UpdatedDate=currentDateTime,
                    UpdatedBy="michaelj" },
                new Asset {
                    Name="Test2",
                    BackEndPlatformID=2,
                    FrontEndPlatformID=1, 
                    ExecutiveSponsorAccountName="michaelj",
                    DataOwnerAccountName="test",
                    DataStewardAccountName="m1kej",
                    CreatedDate=currentDateTime,
                    CreatedBy="michaelj",
                    UpdatedDate=currentDateTime,
                    UpdatedBy="m1kej" }
            };

            context.Assets.AddRange(assets);
            context.SaveChanges();

            var thirdparties = new ThirdParty[]
            {
                new ThirdParty{
                    Name="ThirdParty1",
                    Use="Test1",
                    AssetID=1,
                    CreatedDate=currentDateTime,
                    CreatedBy="michaelj",
                    UpdatedDate=currentDateTime,
                    UpdatedBy="m1kej" },
                new ThirdParty{
                    Name="ThirdParty2",
                    Use="Test2",
                    AssetID=1,
                    CreatedDate=currentDateTime,
                    CreatedBy="m1kej",
                    UpdatedDate=currentDateTime,
                    UpdatedBy="michaelj" },
                new ThirdParty{
                    Name="ThirdParty3",
                    Use="Test3",
                    AssetID=2,
                    CreatedDate=currentDateTime,
                    CreatedBy="michaelj",
                    UpdatedDate=currentDateTime,
                    UpdatedBy="test" },
            };
            context.ThirdParties.AddRange(thirdparties);
            context.SaveChanges();
        }
    }
}