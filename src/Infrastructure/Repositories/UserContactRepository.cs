using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure.Repositories;

public class UserContactRepository : GenericRepository<UserContact>, IUserContactRepository
{
    private readonly AppDbContext _context;
    public UserContactRepository(AppDbContext context) : base(context) { _context = context; }

    public async Task<IEnumerable<UserContact>> GetByUserIdAsync(Guid userId)
        => await _context.UserContacts.Where(c => c.UserId == userId).ToListAsync();
}
