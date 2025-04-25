using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Application.Common;
using DotNetTemplate.Application.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using MediatR;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;

namespace DotNetTemplate.Application.Services;

public class UserContactService : IUserContactService
{
    private readonly IUserContactRepository _userContactRepository;

    public UserContactService(IUserContactRepository userContactRepository)
    {
        _userContactRepository = userContactRepository;
    }

    public async Task<Response<UserContactDto?>> GetByIdAsync(Guid id)
    {
        var contact = await _userContactRepository.GetByIdAsync(id);
        if (contact == null) return new Response<UserContactDto?>("User contact not found.");
        return new Response<UserContactDto?>(contact.Adapt<UserContactDto>(), "Succeeded.");
    }

    public async Task<Response<IEnumerable<UserContactDto>>> GetByUserIdAsync(Guid userId)
    {
        var contacts = await _userContactRepository.GetByUserIdAsync(userId);
        if (contacts == null || !contacts.Any()) return new Response<IEnumerable<UserContactDto>>("No user contacts found.");
        return new Response<IEnumerable<UserContactDto>>(contacts.Adapt<List<UserContactDto>>(), "Succeeded.");
    }

    public async Task<Response<UserContactDto>> CreateAsync(CreateUserContactDto dto)
    {
        var contact = dto.Adapt<UserContact>();
        await _userContactRepository.AddAsync(contact);
        return new Response<UserContactDto>(contact.Adapt<UserContactDto>(), "Succeeded.");
    }
}
