using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Repositories;

public class UserContactRepository : GenericRepository<UserContact>, IUserContactRepository
{
    private readonly AppDbContext _db;
    public UserContactRepository(AppDbContext db) : base(db) { _db = db; }

    public async Task<IEnumerable<UserContact>> GetByUserIdAsync(Guid userId)
        => await _db.UserContacts.Where(c => c.UserId == userId).ToListAsync();
}
