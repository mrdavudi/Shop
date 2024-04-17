using _0_Framework.Application;
using _0_Framework.Application.Auth;
using _0_Framework.Application.PasswordHash;
using AccountManagement.Configuration;
using BlogManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagementConfiguration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using NuGet.Common;
using ServiceHosts;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Shop_DB");

ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstraper.Configure(builder.Services, connectionString);
BlogManagementBootstraper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("AdminArea",
        builder => builder.RequireRole(new List<string> { "1" })));

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCookiePolicy();

app.MapRazorPages();

app.Run();
