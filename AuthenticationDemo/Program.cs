using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
ServiceConfig serviceConfig = builder.Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();

builder.Services.AddHttpClient("auth", hc =>
{
    hc.BaseAddress = new Uri(serviceConfig.AuthServiceBaseAddress);
});
builder.Services.AddHttpClient("rs", hc =>
{
    hc.BaseAddress = new Uri(serviceConfig.ProtectedServiceBaseAddress);
});

//builder.Services.AddHttpClient<AccountService>(client =>
//client.BaseAddress = new Uri(appConfig.AuthBaseAddress));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
