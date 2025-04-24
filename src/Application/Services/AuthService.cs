using DotNetTemplate.Application.DTOs;
using DotNetTemplate.Application.Interfaces;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Application.Common;
using Microsoft.AspNetCore.Identity;

namespace DotNetTemplate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UserDto?> AuthenticateAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null) return null;
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        return result == PasswordVerificationResult.Success ? _mapper.Map<UserDto>(user) : null;
    }
}
