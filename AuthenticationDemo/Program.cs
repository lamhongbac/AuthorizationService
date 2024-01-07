using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ServiceConfig serviceConfig = builder.Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();

builder.Services.AddHttpClient("auth", client =>
{
    client.BaseAddress = new Uri(serviceConfig.AuthServiceBaseAddress);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddHttpClient("rs", client =>
{
    client.BaseAddress = new Uri(serviceConfig.ProtectedServiceBaseAddress);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<AccountService>();
    //client =>
//client.BaseAddress = new Uri(appConfig.AuthBaseAddress));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
