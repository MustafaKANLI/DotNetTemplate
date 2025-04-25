using DotNetTemplate.Application.Common;
using DotNetTemplate.Infrastructure.DTOs;

namespace DotNetTemplate.Application.Services.Interfaces;

public interface IUserService
{
    Task<Response<UserDto?>> GetByIdAsync(Guid id);
    Task<Response<IEnumerable<UserDto>>> GetAllAsync();
    Task<Response<UserDto>> CreateAsync(CreateUserDto dto);
}
