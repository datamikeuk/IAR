// using IAR.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Authorization.Infrastructure;

// namespace IAR.Authorization
// {
//     public class AdminAuthorizationHandler
//                     : AuthorizationHandler<OperationAuthorizationRequirement, Asset>
//     {
//         protected override Task HandleRequirementAsync(
//                                     AuthorizationHandlerContext context,
//                                     OperationAuthorizationRequirement requirement, 
//                                     Asset resource)
//         {
//             if (context.User == null)
//             {
//                 return Task.CompletedTask;
//             }

//             // Administrators can do anything.
//             if (context.User.IsInRole(Constants.AssetAdministratorsRole))
//             {
//                 context.Succeed(requirement);
//             }

//             return Task.CompletedTask;
//         }
//     }
// }