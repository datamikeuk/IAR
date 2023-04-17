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
                userName = principal.Identity.Name;
            }

            var role = _context.Users
                .Where(u => u.Name == userName)
                .Select(u => u.Role).FirstOrDefault();
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = "Role";
            if (!principal.HasClaim(claim => claim.Type == claimType) && role != null)
            {
                claimsIdentity.AddClaim(new Claim(claimType, role));
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}