using DotNetTemplate.Application.DTOs;
using DotNetTemplate.Infrastructure.Interfaces;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Application.Common;

namespace DotNetTemplate.Application.Services;

public class UserContactService : IUserContactService
{
    private readonly IUserContactRepository _userContactRepository;
    private readonly IMapper _mapper;

    public UserContactService(IUserContactRepository userContactRepository, IMapper mapper)
    {
        _userContactRepository = userContactRepository;
        _mapper = mapper;
    }

    public async Task<UserContactDto?> GetByIdAsync(Guid id)
    {
        var contact = await _userContactRepository.GetByIdAsync(id);
        return contact == null ? null : _mapper.Map<UserContactDto>(contact);
    }

    public async Task<IEnumerable<UserContactDto>> GetByUserIdAsync(Guid userId)
    {
        var contacts = await _userContactRepository.GetByUserIdAsync(userId);
        return _mapper.Map<List<UserContactDto>>(contacts);
    }

    public async Task<UserContactDto> CreateAsync(CreateUserContactDto dto)
    {
        var contact = _mapper.Map<UserContact>(dto);
        await _userContactRepository.AddAsync(contact);
        return _mapper.Map<UserContactDto>(contact);
    }
}
