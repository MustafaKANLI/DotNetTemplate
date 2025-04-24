using DotNetTemplate.Application.DTOs;
using DotNetTemplate.Infrastructure.Interfaces;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Application.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DotNetTemplate.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }
}
