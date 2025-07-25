﻿using DotNetTemplate.Application.Services;
using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.Repositories;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using DotNetTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Identity;
using DotNetTemplate.Infrastructure.Identity.Interfaces;

namespace DotNetTemplate.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, bool useInMemory = false)
    {
        if (useInMemory)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("DotNetTemplateDb"));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContactService, UserContactService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IClaimRepository, ClaimRepository>();
        services.AddScoped<IPasswordHasherHelper, PasswordHasherHelper>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserContactRepository, UserContactRepository>();
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITokenHashHelper, TokenHashHelper>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleService, RoleService>();

        return services;
    }
}
