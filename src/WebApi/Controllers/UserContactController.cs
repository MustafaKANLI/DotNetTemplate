using DotNetTemplate.Application.DTOs;
using DotNetTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTemplate.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserContactController : ControllerBase
{
    private readonly IUserContactService _userContactService;

    public UserContactController(IUserContactService userContactService)
    {
        _userContactService = userContactService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var contact = await _userContactService.GetByIdAsync(id);
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var contacts = await _userContactService.GetByUserIdAsync(userId);
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserContactDto dto)
    {
        var contact = await _userContactService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
    }
}
