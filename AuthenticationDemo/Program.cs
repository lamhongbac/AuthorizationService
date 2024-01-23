using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
builder.Services.AddSingleton<MSAUserInfo>();
builder.Services.AddSingleton<MSASignInManager>();


builder.Services.AddControllersWithViews();
ServiceConfig serviceConfig = builder.Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
//auth service
builder.Services.AddHttpClient(AppConstants.AuthenticationService, client =>
{
    client.BaseAddress = new Uri(serviceConfig.AuthServiceBaseAddress);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
});
//resource service
builder.Services.AddHttpClient(AppConstants.ProtectedResourceService, client =>
{
    client.BaseAddress = new Uri(serviceConfig.ProtectedServiceBaseAddress);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpContextAccessor();
//WeUtils

builder.Services.AddSingleton<AccountService>();
//client =>
//client.BaseAddress = new Uri(appConfig.AuthBaseAddress));
//CompanyViewModelHelper
builder.Services.AddSingleton<CompanyViewModelHelper>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//khai bao authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.LogoutPath = "/Home/Logout";
        option.AccessDeniedPath = "/Home/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();=> remove https
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
//2 buoc nay can thuc hien
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
