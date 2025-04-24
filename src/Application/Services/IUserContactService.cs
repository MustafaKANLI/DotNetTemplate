using DotNetTemplate.Application.DTOs;

namespace DotNetTemplate.Application.Services;

public interface IUserContactService
{
    Task<UserContactDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserContactDto>> GetByUserIdAsync(Guid userId);
    Task<UserContactDto> CreateAsync(CreateUserContactDto dto);
}
