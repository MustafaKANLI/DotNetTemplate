using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetTemplate.Infrastructure.Identity;
using System.Text;
using Mapster;

namespace DotNetTemplate.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;
    private readonly JwtHelper _jwtHelper;
    private readonly IClaimService _claimService;

    public AuthController(IAuthService authService, IConfiguration config, JwtHelper jwtHelper, IClaimService claimService)
    {
        _authService = authService;
        _config = config;
        _jwtHelper = jwtHelper;
        _claimService = claimService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _authService.AuthenticateAsync(dto);
        if (user == null) return Unauthorized();
        var claims = await _claimService.GetClaimsByUserIdAsync(user.Data.Id);
        if (claims.Data == null || !claims.Data.Any()) return CreatedAtAction(nameof(Login), null, "No claims found for user.");
        var token = _jwtHelper.GenerateJwtToken(user.Data.Adapt<UserDto>(), claims.Data);

        var response = new LoginResponseDto
        {
            Token = token,
            Expiration = _jwtHelper.GetTokenExpiration(token),
            User = user.Data.Adapt<UserDto>()
        };

        return CreatedAtAction(nameof(Login), new { token }, response);
    }

}
