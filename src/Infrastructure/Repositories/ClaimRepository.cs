using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetTemplate.Infrastructure.Repositories;

public class ClaimRepository : GenericRepository<Claim>, IClaimRepository
{
    private readonly AppDbContext _context;

    public ClaimRepository(AppDbContext context) : base(context){ _context = context; }

    public async Task<IEnumerable<Claim>> GetClaimsByUserIdAsync(Guid userId)
    {
        return await Task.FromResult(_context.Claims.Where(c => c.UserId == userId).ToList());
    }

   
}