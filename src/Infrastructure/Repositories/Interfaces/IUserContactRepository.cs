using DotNetTemplate.Domain.Entities;

namespace DotNetTemplate.Infrastructure.Repositories.Interfaces;

public interface IUserContactRepository : IGenericRepository<UserContact>
{
    Task<IEnumerable<UserContact>> GetByUserIdAsync(Guid userId);
}
