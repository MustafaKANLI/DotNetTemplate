using DotNetTemplate.Application.DTOs;

namespace DotNetTemplate.Application.Services;

public interface IAuthService
{
    Task<UserDto?> AuthenticateAsync(LoginDto dto);
}
