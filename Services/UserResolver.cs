using System.Runtime.Versioning;
using System.DirectoryServices;

public class UserResolver
{
    public readonly IHttpContextAccessor _context;
    public UserResolver(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string GetIdentityName()
    {
        return _context.HttpContext?.User.Identity?.Name??"";
    }

    [SupportedOSPlatform("windows")]
    private string GetCurrentDomainPath()
    {
        DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

        return "LDAP://" + de.Properties["defaultNamingContext"][0]?.ToString();
    }

    [SupportedOSPlatform("windows")]
    public string GetDisplayName()
    {
        DirectorySearcher ds = new DirectorySearcher();
        DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());

        var name = GetIdentityName().Split('\\')[1]; // Get the username from the domain
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
        DirectoryEntry de = new DirectoryEntry(GetCurrentDomainPath());

        var name = GetIdentityName().Split('\\')[1]; // Get the username from the domain
        ds = new DirectorySearcher(de);
        ds.Filter = "(&(objectClass=user)(objectcategory=person)(sAMAccountName=" + name + "))";
        SearchResult? userProperty = ds.FindOne();

        var userEmail = userProperty?.Properties["mail"][0].ToString();
        return userEmail??"";
    }
}