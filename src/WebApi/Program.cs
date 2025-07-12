using Microsoft.OpenApi.Models;
using DotNetTemplate.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DotNetTemplate.Application.Common;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerUI;
using DotNetTemplate.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
MapsterConfig.RegisterMappings();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(x => x.ToString());
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotNetTemplate WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            new List<string>()
                        }
                    });
});
var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");
var dbProvider = builder.Configuration.GetValue<string>("DatabaseProvider");

string connectionString = dbProvider switch
{
    "MSSQL" => builder.Configuration.GetConnectionString("MSSQL"),
    "PostgreSQL" => builder.Configuration.GetConnectionString("PostgreSQL"),
    _ => throw new Exception("DatabaseProvider must be MSSQL or PostgreSQL")
};

builder.Services.AddInfrastructure(connectionString, useInMemory);

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "supersecretkey";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
      builder =>
      {
          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetTemplate WebApi V1");
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
