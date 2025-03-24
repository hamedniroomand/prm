using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRM.API.Common;
using PRM.Application;
using PRM.Application.Common;
using PRM.Application.Models;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthConstants.SuperAdminUserPolicyName, p => p.RequireRole(UserRole.SuperAdmin.ToString()))
    .AddPolicy(AuthConstants.AdminUserPolicyName, p => p.RequireRole(UserRole.Admin.ToString()))
    .AddPolicy(AuthConstants.EmployeePolicyName, p => p.RequireClaim(UserRole.Employee.ToString()));

builder.Services.AddSingleton(new ApplicationSettings(Environment.GetEnvironmentVariable("JWT_SECRET")!));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    x.TagActionsBy(apiDesc =>
    {
        var groupName = apiDesc.GroupName ?? apiDesc.ActionDescriptor.RouteValues["controller"];
        return [groupName];
    });
    x.DocInclusionPredicate((docName, apiDesc) => true);
});

builder.Services.AddDatabase(Environment.GetEnvironmentVariable("DATABASE_CONNECTION")!);
builder.Services.AddApplication();


var app = builder.Build();

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