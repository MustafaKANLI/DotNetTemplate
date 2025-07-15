using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Application.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mapster;
using MediatR;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.Identity;

namespace DotNetTemplate.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response<UserDto?>> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return new Response<UserDto?>("User not found.");
        return new Response<UserDto>(user.Adapt<UserDto>(), "Succeeded.");
    }

    public async Task<Response<IEnumerable<UserDto>>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        if (users == null || !users.Any()) return new Response<IEnumerable<UserDto>>("No users found.");
        return new Response<IEnumerable<UserDto>>(users.Adapt<List<UserDto>>(), "Succeeded.");
    }

    public async Task<Response<UserDto>> CreateAsync(CreateUserDto dto)
    {
        var user = dto.Adapt<User>();

        var helper = new PasswordHasherHelper();
        user.PWHash = Convert.FromBase64String(helper.HashPassword(dto.Password));

        await _userRepository.AddAsync(user);
        return new Response<UserDto>(user.Adapt<UserDto>(), "Succeeded.");
    }
}
