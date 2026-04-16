using LB_POS.Core;
using LB_POS.Core.Middleware;
using LB_POS.Data.Entities.Identity;
using LB_POS.Infrastructure;
using LB_POS.Infrastructure.Data;
using LB_POS.Infrastructure.Seeder;
using LB_POS.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartBreadcrumbs.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ================== Services ==================

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Login");
}).AddRazorRuntimeCompilation();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpContextAccessor();
// Project Layers
builder.Services.AddInfrastructureDependencies()
                 .AddServiceDependencies()
                 .AddCoreDependencies()
                 .AddServiceRegisteration(builder.Configuration);

#region ✅ Localization

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "";
});


builder.Services.AddBreadcrumbs(typeof(Program).Assembly);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("ar-EG"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("ar-EG");

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // مهم جداً: خلي الكوكي الأول بدون حذف الباقي
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

#endregion

// Helpers
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Login";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.AccessDeniedPath = "/Shared/AccessDenied";
});
builder.Services.AddAuthorization();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("auth", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://your-frontend-domain.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
var app = builder.Build();

// ================== Seed ==================
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager, roleManager);
}

// ================== Middleware ==================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// ✅ مهم جداً: قبل Routing
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowSpecificOrigins");   // مهم
app.UseRateLimiter();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ================== Endpoints ==================

app.MapRazorPages().RequireAuthorization();
app.MapControllers();
app.MapPost("/logout", [Authorize] async (HttpContext http) =>
{
    await http.SignOutAsync(IdentityConstants.ApplicationScheme);
    return Results.Redirect("/Login");
});
app.Run();