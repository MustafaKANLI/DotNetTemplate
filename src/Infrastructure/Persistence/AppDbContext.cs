using DotNetTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace DotNetTemplate.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserContact> UserContacts => Set<UserContact>();
    public DbSet<Claim> Claims => Set<Claim>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var useMssql = _configuration.GetValue<bool>("UseMSSQL");
        if (useMssql)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL"));
        }
        else
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
           .SelectMany(t => t.GetProperties())
           .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(6);
        }

        base.OnModelCreating(modelBuilder);
    }

}
