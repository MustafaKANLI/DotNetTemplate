using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.DTOs;

namespace DotNetTemplate.Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);

}
