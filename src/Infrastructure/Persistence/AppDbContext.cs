using DotNetTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserContact> UserContacts => Set<UserContact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
           .SelectMany(t => t.GetProperties())
           .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18); // Replaced SetColumnType with SetPrecision  
            property.SetScale(6);      // Added SetScale for decimal scale  
        }

        base.OnModelCreating(modelBuilder);
    }
}
