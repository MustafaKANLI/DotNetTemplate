using DotNetTemplate.Domain.Entities;

namespace DotNetTemplate.Infrastructure.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByIdAsync(Guid id);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task RevokeAsync(RefreshToken token, string revokedByIp);
    Task SaveChangesAsync();
}
