using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IAR.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using IAR.Authorization;
// using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RegisterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RegisterContext") ?? throw new InvalidOperationException("Connection string 'RegisterContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RegisterContext>();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// Authorization handlers
builder.Services.AddScoped<IAuthorizationHandler,
                      UserIsOwnerAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationHandler,
                      AdminAuthorizationHandler>();

// builder.Services.AddWebOptimizer();

// builder.Services.AddRazorPages(options =>
//     {
//         options.Conventions.AddPageRoute(
//             "/Home", "");
//     });

var app = builder.Build();

app.UsePathBase("/IAR");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    // app.UseWebOptimizer();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    // builder.Services.AddWebOptimizer(minifyJavaScript:false,minifyCss:false);
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

// var options = new RewriteOptions()
//     .AddRedirect("(.*[^/])$", "$1/");

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();