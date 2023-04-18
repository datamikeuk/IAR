using System.Runtime.Versioning;
using System.DirectoryServices;

public class UserResolver
{
    public readonly IHttpContextAccessor _context;
    private readonly IConfiguration _configuration;
    public UserResolver(IHttpContextAccessor context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public string GetIdentityName()
    {
        return _context.HttpContext?.User.Identity?.Name??"";
    }

    public string GetUserName()
    {
        return GetIdentityName().Split('\\')[1];;
    }

    [SupportedOSPlatform("windows")]
    public string GetCurrentDomainPath()
    {
        DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

        return "LDAP://" + de.Properties["defaultNamingContext"][0]?.ToString();
    }

    [SupportedOSPlatform("windows")]
    public string GetDisplayName()
    {
        DirectorySearcher ds = new DirectorySearcher();
        DirectoryEntry de = new DirectoryEntry(_configuration.GetValue<string>("LDAP"));

        var name = GetUserName();
        ds = new DirectorySearcher(de);
        ds.Filter = "(&(objectClass=user)(objectcategory=person)(sAMAccountName=" + name + "))";
        SearchResult? userProperty = ds.FindOne();

        // var userEmail = userProperty?.Properties["mail"][0];
        var userName = userProperty?.Properties["displayname"][0].ToString();
        return userName??"";
    }

        [SupportedOSPlatform("windows")]
    public string GetEmail()
    {
        DirectorySearcher ds = new DirectorySearcher();
        DirectoryEntry de = new DirectoryEntry(_configuration.GetValue<string>("LDAP"));

        var name = GetUserName();
        ds = new DirectorySearcher(de);
        ds.Filter = "(&(objectClass=user)(objectcategory=person)(sAMAccountName=" + name + "))";
        SearchResult? userProperty = ds.FindOne();

        var userEmail = userProperty?.Properties["mail"][0].ToString();
        return userEmail??"";
    }
}