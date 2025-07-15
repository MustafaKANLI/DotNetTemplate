using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Application.Common;

namespace DotNetTemplate.Application.Services.Interfaces;

public interface IRoleService
{
    Task<Response<IEnumerable<RoleDto>>> GetAllAsync();
    Task<Response<RoleDto?>> GetByIdAsync(Guid id);
    Task<Response<RoleDto>> CreateAsync(CreateRoleDto dto);
    Task<Response<bool>> DeleteAsync(Guid id);
}
