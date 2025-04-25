using DotNetTemplate.Application.Common;
using DotNetTemplate.Infrastructure.DTOs;

namespace DotNetTemplate.Application.Services.Interfaces;

public interface IUserContactService
{
    Task<Response<UserContactDto>?> GetByIdAsync(Guid id);
    Task<Response<IEnumerable<UserContactDto>>> GetByUserIdAsync(Guid userId);
    Task<Response<UserContactDto>> CreateAsync(CreateUserContactDto dto);
}
