using System.Diagnostics;
using System.DirectoryServices;
using System.Runtime.Versioning;
using IAR.Data;
using IAR.Models;

public class UpdateUsersHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UpdateUsersHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    [SupportedOSPlatform("windows")]
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            Debug.WriteLine("Updating users");
            UpdateUsers();
            // Run every hour
            await Task.Delay(3600000, stoppingToken);
        }
    }
    
    [SupportedOSPlatform("windows")]
    public void UpdateUsers()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<RegisterContext>();
            const string LDAP = "LDAP://OU=User Accounts,OU=Honeycomb Group,DC=Staffshousing,DC=org,DC=uk";
            SearchResultCollection results;
            DirectorySearcher ds = new DirectorySearcher();
            DirectoryEntry de = new DirectoryEntry(LDAP);
            
            ds = new DirectorySearcher(de);
            ds.Filter = "(&(objectCategory=User)(objectClass=person))";
            
            results = ds.FindAll();

            foreach (SearchResult sr in results)
            {
                var accountName = sr.Properties["sAMAccountName"][0].ToString();
                var displayName = sr.Properties["displayname"][0].ToString();
                if (accountName != null && displayName != null && !displayName.Contains("Voicemail"))
                {
                    var existingUser = dbContext.Users.Find(accountName);
                    if (existingUser == null)
                    {
                        var userEntry = new User { AccountName = accountName, DisplayName = displayName };
                        dbContext.Users.Add(userEntry);
                    }
                    else
                    {
                        existingUser.DisplayName = displayName;
                    }
                }
            }
            dbContext.SaveChanges();
        }
    }
}