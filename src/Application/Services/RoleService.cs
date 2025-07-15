using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using DotNetTemplate.Application.Common;
using DotNetTemplate.Domain.Entities;
using Mapster;

namespace DotNetTemplate.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Response<IEnumerable<RoleDto>>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return new Response<IEnumerable<RoleDto>>(roles.Adapt<List<RoleDto>>());
    }

    public async Task<Response<RoleDto?>> GetByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null) return new Response<RoleDto?>("Role not found.");
        return new Response<RoleDto?>(role.Adapt<RoleDto>());
    }

    public async Task<Response<RoleDto>> CreateAsync(CreateRoleDto dto)
    {
        var existing = await _roleRepository.GetByNameAsync(dto.Name);
        if (existing != null) return new Response<RoleDto>("Role already exists.");

        var role = dto.Adapt<Role>();
        await _roleRepository.AddAsync(role);

        return new Response<RoleDto>(role.Adapt<RoleDto>(), "Succeeded.");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id)
    {
        await _roleRepository.DeleteAsync(id);
        return new Response<bool>(true, "Deleted.");
    }
}
