using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace IAR.Pages
{
    [AllowAnonymous]
    public class UserModel : PageModel
    {
        private readonly ILogger<UserModel> _logger;

        public UserModel(ILogger<UserModel> logger)
        {
            _logger = logger;
        }

        public List<String> Roles { get; set; } = null!;

        [SupportedOSPlatform("windows")]
        public void OnGet()
        {
            // if (User.Identity != null) { GetADRoles(User.Identity); }
        }

        [SupportedOSPlatform("windows")]
        public List<String> GetADRoles(System.Security.Principal.IIdentity user)
        {
            var identity = (ClaimsIdentity)user;
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

            return new List<string>(Roles);
        }
    }
}