using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetTemplate.Infrastructure.Identity;
using System.Text;
using Mapster;
using DotNetTemplate.Infrastructure.Identity.Interfaces;

namespace DotNetTemplate.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;
    private readonly IJwtHelper _jwtHelper;
    private readonly IClaimService _claimService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, IConfiguration config, IJwtHelper jwtHelper, ITokenService tokenService, IClaimService claimService)
    {
        _authService = authService;
        _config = config;
        _jwtHelper = jwtHelper;
        _tokenService = tokenService;
        _claimService = claimService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _authService.AuthenticateAsync(dto);
        if (user == null || user.Data == null) return Unauthorized();

        var claims = await _claimService.GetClaimsByUserIdAsync(user.Data.Id);
        if (claims.Data == null || !claims.Data.Any())
            return Unauthorized("No claims found for user.");

        // ACCESS + REFRESH TOKEN üret
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var (accessToken, refreshToken) = await _tokenService.GenerateTokensAsync(user.Data, ipAddress);

        // RefreshToken cookie olarak set et
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            SameSite = SameSiteMode.Strict
        });

        var response = new LoginResponseDto
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            Expiration = _jwtHelper.GetTokenExpiration(accessToken),
            User = user.Data.Adapt<UserDto>()
        };

        return Ok(response);
    }


    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return Unauthorized("No refresh token cookie found.");

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var (newAccessToken, newRefreshToken) = await _tokenService.RefreshAsync(refreshToken, ipAddress);

        // Yeni refresh cookie setle
        Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            SameSite = SameSiteMode.Strict
        });

        return Ok(new
        {
            Token = newAccessToken,
            Expiration = _jwtHelper.GetTokenExpiration(newAccessToken)
        });
    }


    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return BadRequest("No refresh token cookie found.");

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        await _tokenService.RevokeAsync(refreshToken, ipAddress);

        // Cookie'yi sil
        Response.Cookies.Delete("refreshToken");

        return Ok();
    }


}
