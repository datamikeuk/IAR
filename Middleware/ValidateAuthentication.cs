using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace IAR.Middleware;

// Customer middleware to validate authentication when using Kestrel
public class ValidateAuthentication : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity != null)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                Console.WriteLine("User is authenticated");
                await next(context);
            }
            else
            {
                Console.WriteLine("User is not authenticated");
                await context.ChallengeAsync();
            }
        }
        else
        {
            Console.WriteLine("User is null");
        }
    }

    private bool HasAnonymousAttribute(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var retVal = (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null);

        return retVal;
    }
}
public static class ValidateAuthenticationExtensions
{
    public static IApplicationBuilder UseValidateAuthentication(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidateAuthentication>();
    }
}