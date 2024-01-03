using AuthorizationService.Data;
using AuthorizationService.Helper;
using AuthorizationService.Service;
using AuthServices;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ApiKeyAuthFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
JwtConfig jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
DBConfiguration dBConfiguration = builder.Configuration.GetSection("DBConfiguration").Get<DBConfiguration>();
builder.Services.AddAutoMapper(typeof(ModelMappingProfile));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //tu cap token?
            ValidateIssuer = true,
            ValidateAudience = true,

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),

        };
    });

builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton(dBConfiguration);
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

builder.Services.AddSingleton<ApplicationService>(x => new ApplicationService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<AppObjectService>(x => new AppObjectService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<AppRoleService>(x => new AppRoleService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<AppUserService>(x => new AppUserService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<CompanyApplicationService>(x => new CompanyApplicationService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<CompanyService>(x => new CompanyService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<RoleRightService>(x => new RoleRightService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<UserRoleService>(x => new UserRoleService(dBConfiguration.ConnectionString));
builder.Services.AddSingleton<UserStoreService>(x => new UserStoreService(dBConfiguration.ConnectionString));
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(IdentityData.AdminUserPolicyName, p => p.RequireClaim(IdentityData.AdminUserClaimName));
});
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
