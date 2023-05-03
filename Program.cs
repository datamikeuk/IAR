using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using IAR.Data;
using IAR.Services;
using IAR.Apis;
using IAR.Middleware;
using IAR.Models;
using System.Globalization;
using Audit.Core;
using System.Text.Json;

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
// Background service to update users from AD
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

// app.UsePathBase("/IAR");

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

Audit.Core.Configuration.AuditDisabled = true;

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
app.UseValidateAuthentication();

app.MapRazorPages();

Apis.GetApis(app);

Audit.Core.Configuration.AuditDisabled = false;

var _userResolver = builder.Services.BuildServiceProvider().GetService<UserResolver>();

Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
{
    scope.Event.Environment.UserName = _userResolver?.GetUserName();
});

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
            entity.Changes = JsonSerializer.Serialize(entry.Changes);
            entity.Date = DateTime.Now;
            entity.User = Environment.UserName;
        })
	.IgnoreMatchedProperties(true));

app.Run();