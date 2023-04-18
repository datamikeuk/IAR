using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddSingleton<ValidateAuthentication>();

// Set the fallback authorization policy to require users to be authenticated
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<UserResolver>();
// builder.Services.AddHostedService<UpdateUsersHostedService>();

// Authorization handlers
// builder.Services.AddScoped<IAuthorizationHandler,
//                       UserIsOwnerAuthorizationHandler>();

// builder.Services.AddSingleton<IAuthorizationHandler,
//                       AdminAuthorizationHandler>();

builder.Services.AddDbContext<RegisterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegisterContext") ?? 
        throw new InvalidOperationException("Connection string 'RegisterContext' not found.")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UsePathBase("/IAR");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RegisterContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidateAuthentication>();

app.MapRazorPages();

app.Run();

// Customer middleware to validate authentication when using Kestrel
internal class ValidateAuthentication : IMiddleware
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