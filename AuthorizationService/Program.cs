using AuthorizationService.DataTypes;
using AuthorizationService.Helper;
using AuthorizationService.Service;
using AuthServices;
using AuthServices.Models;

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MSASharedLib.DataTypes;

using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddControllers();
//builder.Services.AddScoped<ApiKeyAuthFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOption>();

JwtConfig jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.AddAutoMapper(typeof(ModelMappingProfile));


builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton<AuthJwtUtil>();

builder.Services.AddSingleton<RefreshTokenDatas>();
builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddSingleton<AccountService>();

builder.Services.AddSingleton<ApplicationService>();
builder.Services.AddSingleton<AppObjectService>();
builder.Services.AddSingleton<AppRoleService>();
builder.Services.AddSingleton<AppUserService>();
builder.Services.AddSingleton<CompanyApplicationService>();
builder.Services.AddSingleton<CompanyService>();
builder.Services.AddSingleton<RoleRightService>();
builder.Services.AddSingleton<UserRoleService>();
builder.Services.AddSingleton<UserStoreService>();
//builder.Services.AddAuthorization(x =>
//{
//    x.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName));
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
