using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Persistence;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetTemplate.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetTemplate.Infrastructure.Identity.Interfaces;

namespace DotNetTemplate.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;
    private readonly ITokenHashHelper _hasher;


    public RefreshTokenRepository(AppDbContext context, ITokenHashHelper hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id)
    {
        return await _context.RefreshTokens.FindAsync(id);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string hashedToken)
    {
        // Tüm tokenlar içinde hash karşılaştır
        var tokens = await _context.RefreshTokens
            .Where(t => t.RevokedAt == null)
            .ToListAsync();

        return tokens.FirstOrDefault(t => _hasher.Verify(hashedToken, t.TokenHash));
    }

    public async Task RevokeAsync(RefreshToken token, string revokedByIp)
    {
        token.RevokedAt = DateTime.UtcNow;
        token.RevokedByIp = revokedByIp;
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
