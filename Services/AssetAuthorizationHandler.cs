using IAR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IAR.Services
{
    public class AssetAuthorizationHandler
                : AuthorizationHandler<ResponsibleRequirement, Asset>
    {
        private readonly UserResolver _userResolver;

        public AssetAuthorizationHandler(UserResolver userResolver)
        {
            _userResolver = userResolver;
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

            var accountName = _userResolver.GetAccountName();
            var executiveSponsorAccountName = resource.ExecutiveSponsorAccountName?.ToString();
            var dataOwnerAccountName = resource.DataOwnerAccountName?.ToString();
            var dataStewardAccountName = resource.DataStewardAccountName?.ToString();
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