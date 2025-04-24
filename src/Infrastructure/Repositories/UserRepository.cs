using DotNetTemplate.Infrastructure.Interfaces;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) { _db = db; }

    public async Task<User?> GetByIdAsync(Guid id) => await _db.Users.Include(u => u.Contacts).FirstOrDefaultAsync(u => u.Id == id);
    public async Task<User?> GetByUsernameAsync(string username) => await _db.Users.Include(u => u.Contacts).FirstOrDefaultAsync(u => u.Username == username);
    public async Task<IEnumerable<User>> GetAllAsync() => await _db.Users.Include(u => u.Contacts).ToListAsync();
    public async Task AddAsync(User user) { _db.Users.Add(user); await _db.SaveChangesAsync(); }
}

public class UserContactRepository : IUserContactRepository
{
    private readonly AppDbContext _db;
    public UserContactRepository(AppDbContext db) { _db = db; }

    public async Task<UserContact?> GetByIdAsync(Guid id) => await _db.UserContacts.FindAsync(id);
    public async Task<IEnumerable<UserContact>> GetByUserIdAsync(Guid userId) => await _db.UserContacts.Where(c => c.UserId == userId).ToListAsync();
    public async Task AddAsync(UserContact contact) { _db.UserContacts.Add(contact); await _db.SaveChangesAsync(); }
}
