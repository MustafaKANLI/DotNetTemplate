using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) : base(db) { _db = db; }

    public async Task<User?> GetByUsernameAsync(string username)
        => await _db.Users.Include(u => u.Contacts).FirstOrDefaultAsync(u => u.Username == username);
}
