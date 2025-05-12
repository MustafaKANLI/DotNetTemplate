using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Application.Services.Interfaces;
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
        var result = await _userContactService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var result = await _userContactService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserContactDto dto)
    {
        var result = await _userContactService.CreateAsync(dto);
        return Ok(result);
    }
}
