using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using IAR.Data;

namespace IAR.Services
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly RegisterContext _context;

        public ClaimsTransformer(RegisterContext context)
        {
            _context = context;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity;
            var userName = identity?.Name?.Split('\\')[1];
            var accountName = userName?.ToLower();

            var role = _context.Roles
                .Where(r => r.AccountName == accountName)
                .Select(r => r.RoleName).FirstOrDefault();
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            var claimType = "Role";
            if (!principal.HasClaim(claim => claim.Type == claimType) && role != null)
            {
                claimsIdentity.AddClaim(new Claim(claimType, role));
            }

            principal.AddIdentity(claimsIdentity);
            
            claimType = "AccountName";
            if (!principal.HasClaim(claim => claim.Type == claimType) && accountName != null)
            {
                claimsIdentity.AddClaim(new Claim(claimType, accountName));
            }

            principal.AddIdentity(claimsIdentity);

            var displayName = _context.Users
                .Where(u => u.AccountName == accountName)
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