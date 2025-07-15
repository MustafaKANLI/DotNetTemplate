using DotNetTemplate.Domain.Entities;

namespace DotNetTemplate.Infrastructure.Repositories.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
}
