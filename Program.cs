using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Services;
using IAR.Apis;
using IAR.Middleware;
using IAR.Models;
using Audit.Core;
// using System.Globalization;
// using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddSingleton<ValidateAuthentication>();

builder.Services.AddHttpContextAccessor();
// builder.Services.AddSingleton<UserResolver>();
builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
builder.Services.AddTransient<UserTableLookup>();

// Set the fallback authorization policy to require users to be authenticated
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("Responsible", policy => policy.AddRequirements(new ResponsibleRequirement()));
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// Authorization handlers
builder.Services.AddScoped<IAuthorizationHandler, AssetAuthorizationHandler>();

// Background service to update users from AD - production only
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHostedService<UpdateUsersHostedService>();
}

builder.Services.AddDbContext<RegisterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegisterContext") ?? 
        throw new InvalidOperationException("Connection string 'RegisterContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// app.UsePathBase("/IAR");

Audit.Core.Configuration.AuditDisabled = true;

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RegisterContext>();
    // context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
// Run the custom middleware to validate authentication only when in development
// This is for Kestrel only
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ValidateAuthentication>();
}

app.MapRazorPages();

Apis.GetApis(app);

Audit.Core.Configuration.AuditDisabled = false;

// Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
// {
//     scope.Event.Environment.UserName = app.Services.GetService<UserResolver>()?.GetAccountName();
// });

Audit.Core.Configuration.Setup()
    .UseEntityFramework(_ => _
        .AuditTypeMapper(t => typeof(AuditLog))  
        .AuditEntityAction<AuditLog>((ev, entry, entity) =>
        {
            // entity.EntityType = entry.EntityType.Name;
            entity.Table = entry.Table;
	        entity.TableID = entry.PrimaryKey.First().Value as int? ?? 0;
            entity.Action = entry.Action;
            entity.AuditData = entry.ToJson();
            // entity.Changes = JsonSerializer.Serialize(entry.Changes);
            entity.Date = DateTime.Now;
            entity.User = Environment.UserName;
        })
	.IgnoreMatchedProperties(true));

app.Run();