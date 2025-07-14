using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DotNetTemplate.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ClaimController : ControllerBase
{
    private readonly IClaimService _claimService;

    public ClaimController(IClaimService claimService)
    {
        _claimService = claimService;
    }

    [Authorize(Policy = "AdminGetAccess")]
    [HttpGet()]
    public async Task<IActionResult> GetClaimsByUserId(Guid userId)
    {
        var claims = await _claimService.GetClaimsByUserIdAsync(userId);
        return Ok(claims);
    }

    [HttpPost()]
    public async Task<IActionResult> AddClaim([FromBody] ClaimDto dto)
    {
        var result = await _claimService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteClaim(Guid userId, string claimType)
    {
        var result = await _claimService.DeleteAsync(userId, claimType);
        return Ok(result);
    }
}