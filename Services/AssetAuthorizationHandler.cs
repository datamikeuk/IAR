using IAR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IAR.Services
{
    public class AssetAuthorizationHandler
                : AuthorizationHandler<ResponsibleRequirement, Asset>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AssetAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   ResponsibleRequirement requirement,
                                   Asset resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            var accountName = _httpContextAccessor.HttpContext?.User.FindFirst("AccountName")?.Value;
            var executiveSponsorAccountName = resource.ExecutiveSponsorAccountName;
            var dataOwnerAccountName = resource.DataOwnerAccountName;
            var dataStewardAccountName = resource.DataStewardAccountName;
            var isAdmin = context.User.FindFirst("Role")?.Value == "Admin";
                
            if (isAdmin || accountName == executiveSponsorAccountName || 
                accountName == dataOwnerAccountName || accountName == dataStewardAccountName )
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ResponsibleRequirement : IAuthorizationRequirement {}
}