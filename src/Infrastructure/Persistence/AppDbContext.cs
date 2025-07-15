using DotNetTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotNetTemplate.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Parametresiz ctor for EF CLI
    public AppDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // EF CLI'da veya parametresiz ctor'da çalışırken
            // appsettings.json'u elle yükle
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebApi"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var provider = config["DatabaseProvider"] ?? "MSSQL";

            if (provider == "MSSQL")
            {
                var connectionString = config.GetConnectionString("MSSQL");
                optionsBuilder.UseSqlServer(connectionString);
            }
            else if (provider == "PostgreSQL")
            {
                var connectionString = config.GetConnectionString("PostgreSQL");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserContact> UserContacts => Set<UserContact>();
    public DbSet<Claim> Claims => Set<Claim>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Role> Roles => Set<Role>();

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
