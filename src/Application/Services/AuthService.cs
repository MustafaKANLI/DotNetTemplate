using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using DotNetTemplate.Application.Common;
using DotNetTemplate.Infrastructure.Identity;
using Mapster;
using MediatR;
using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.Identity.Interfaces;

namespace DotNetTemplate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherHelper _passwordHasher;

    public AuthService(IUserRepository userRepository, IPasswordHasherHelper passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response<UserDto?>> AuthenticateAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null) throw new UnauthorizedAccessException("Invalid username or password.");
        var result = _passwordHasher.VerifyPassword(Convert.ToBase64String(user.PWHash), dto.Password);
        if (result)
        {
            return new Response<UserDto>(user.Adapt<UserDto>(), "Succeeded.");
        }
        return new Response<UserDto>();
    }
}
