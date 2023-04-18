using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using IAR.Data;

namespace IAR.Services
{
    public class MyClaimsTransformation : IClaimsTransformation
    {
        private readonly RegisterContext _context;

        public MyClaimsTransformation(RegisterContext context)
        {
            _context = context;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var userName = "";
            if (principal.Identity != null)
            {
                userName = principal.Identity.Name?.Split('\\')[1];;
            }

            var role = _context.Roles
                .Where(r => r.AccountName == userName)
                .Select(r => r.RoleName).FirstOrDefault();
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = "Role";
            if (!principal.HasClaim(claim => claim.Type == claimType) && role != null)
            {
                claimsIdentity.AddClaim(new Claim(claimType, role));
            }

            principal.AddIdentity(claimsIdentity);

            var displayName = _context.Users
                .Where(u => u.AccountName == userName)
                .Select(u => u.DisplayName).FirstOrDefault();
            
            claimType = "DisplayName";
            if (!principal.HasClaim(claim => claim.Type == claimType) && displayName != null)
            {
                claimsIdentity.AddClaim(new Claim(claimType, displayName));
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}