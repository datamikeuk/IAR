using System.Runtime.Versioning;
using System.DirectoryServices;
using IAR.Data;

public class UserResolver
{
    public readonly IHttpContextAccessor _context;
    private readonly IConfiguration _configuration;
    // private readonly RegisterContext _db;
    public UserResolver(IHttpContextAccessor context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        // _db = db;
    }

    public string GetAccountName()
    {
        // return GetUserName().ToLower();
        var accountName = _context.HttpContext?.User.Identity?.Name??"";
        var accountNameSplit = accountName.Split('\\');
        if (accountNameSplit.Length > 1)
        {
            return accountNameSplit[1].ToLower();
        }
        else
        {
            return "error";
        }
    }

    // Lookup Display Name from User table based on account name
    // public string GetDisplayName(String accountName)
    // {
    //     var displayName = _db.Users
    //         .Where(u => u.AccountName == accountName)
    //         .Select(u => u.DisplayName).FirstOrDefault();
    //     return displayName??"";
    // }

    // [SupportedOSPlatform("windows")]
    // public string GetCurrentDomainPath()
    // {
    //     DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

    //     return "LDAP://" + de.Properties["defaultNamingContext"][0]?.ToString();
    // }

    // [SupportedOSPlatform("windows")]
    // public string GetDisplayName()
    // {
    //     DirectorySearcher ds = new DirectorySearcher();
    //     DirectoryEntry de = new DirectoryEntry(_configuration.GetValue<string>("LDAP"));

    //     var name = GetUserName();
    //     ds = new DirectorySearcher(de);
    //     ds.Filter = "(&(objectClass=user)(objectcategory=person)(sAMAccountName=" + name + "))";
    //     SearchResult? userProperty = ds.FindOne();

    //     // var userEmail = userProperty?.Properties["mail"][0];
    //     var userName = userProperty?.Properties["displayname"][0].ToString();
    //     return userName??"";
    // }

    // [SupportedOSPlatform("windows")]
    // public string GetEmail()
    // {
    //     DirectorySearcher ds = new DirectorySearcher();
    //     DirectoryEntry de = new DirectoryEntry(_configuration.GetValue<string>("LDAP"));

    //     var name = GetUserName();
    //     ds = new DirectorySearcher(de);
    //     ds.Filter = "(&(objectClass=user)(objectcategory=person)(sAMAccountName=" + name + "))";
    //     SearchResult? userProperty = ds.FindOne();

    //     var userEmail = userProperty?.Properties["mail"][0].ToString();
    //     return userEmail??"";
    // }
}