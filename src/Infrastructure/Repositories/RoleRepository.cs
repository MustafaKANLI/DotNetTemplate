using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
    }
}
