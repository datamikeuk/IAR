using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IAR.Pages;

[AllowAnonymous]
public class UserModel : PageModel
{
    private readonly ILogger<AboutModel> _logger;

    public UserModel(ILogger<AboutModel> logger)
    {
        _logger = logger;
    }

    public List<String> Roles { get; set; } = null!;

    public void OnGet()
    {
        GetADRoles();
    }

    public void GetADRoles()
    {
        if (User.Identity != null){
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            // var name = identity.Name;
            // var email = claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
            // var role = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();

            Roles = new List<String>();
            var roles = claims
                .Where(q => q.Type == ClaimTypes.GroupSid)
                .Select(q => q.Value);

            foreach (var role in roles)
            {
                var name = new System.Security.Principal.SecurityIdentifier(role)
                    .Translate(typeof(System.Security.Principal.NTAccount)).ToString();
                Roles.Add(name);
            }
        }
    }
}