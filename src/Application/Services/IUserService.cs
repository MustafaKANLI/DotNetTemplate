using DotNetTemplate.Application.DTOs;

namespace DotNetTemplate.Application.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> CreateAsync(CreateUserDto dto);
}
