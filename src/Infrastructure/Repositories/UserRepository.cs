using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private new readonly AppDbContext _context;
    public UserRepository(AppDbContext context) : base(context) { _context = context; }

    public async Task<User?> GetByUsernameAsync(string username)
        => await _context.Users.Include(u => u.Contacts).FirstOrDefaultAsync(u => u.Username == username);
}
