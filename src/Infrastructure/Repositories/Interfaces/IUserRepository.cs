using DotNetTemplate.Domain.Entities;

namespace DotNetTemplate.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
}

public interface IUserContactRepository
{
    Task<UserContact?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserContact>> GetByUserIdAsync(Guid userId);
    Task AddAsync(UserContact contact);
}
