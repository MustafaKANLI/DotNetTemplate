using DotNetTemplate.Application.Common;
using DotNetTemplate.Infrastructure.DTOs;

namespace DotNetTemplate.Application.Services.Interfaces;

public interface IAuthService
{
    Task<Response<UserDto?>> AuthenticateAsync(LoginDto dto);
}
