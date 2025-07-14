using DotNetTemplate.Application.Services.Interfaces;
using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Infrastructure.Repositories.Interfaces;
using DotNetTemplate.Application.Common;
using DotNetTemplate.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetTemplate.Infrastructure.Identity.Interfaces;
using DotNetTemplate.Infrastructure.Identity;

namespace DotNetTemplate.Application.Services;

public class TokenService : ITokenService
{
    private readonly IUserService _userService;
    private readonly IClaimService _claimService;
    private readonly IRefreshTokenRepository _refreshRepo;
    private readonly IJwtHelper _jwtGenerator;
    private readonly ITokenHashHelper _hasher;

    public TokenService(
        IUserService userService,
        IClaimService claimService,
        IRefreshTokenRepository refreshRepo,
        IJwtHelper jwtGenerator,
        ITokenHashHelper hasher)
    {
        _userService = userService;
        _claimService = claimService;
        _refreshRepo = refreshRepo;
        _jwtGenerator = jwtGenerator;
        _hasher = hasher;
    }

    public async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(UserDto user, string ipAddress)
    {
        var claims = await _claimService.GetClaimsByUserIdAsync(user.Id);
        if (claims.Data == null || !claims.Data.Any())
            return (string.Empty, string.Empty);
        // 1. Access Token
        var accessToken = _jwtGenerator.GenerateJwtToken(user, claims.Data);

        // 2. Refresh Token
        var refreshTokenRaw = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        var refreshTokenHash = _hasher.Hash(refreshTokenRaw);

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = refreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedByIp = ipAddress
        };

        await _refreshRepo.AddAsync(refreshTokenEntity);

        return (accessToken, refreshTokenRaw);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken, string ipAddress)
    {
        var tokenEntity = await _refreshRepo.GetByTokenAsync(refreshToken);
        if (tokenEntity == null || tokenEntity.RevokedAt != null || tokenEntity.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid refresh token");

        Response<UserDto?> userResponse = await _userService.GetByIdAsync(tokenEntity.UserId);
        UserDto? user = userResponse.Data.Adapt<UserDto>();

        // ROTATION: eski token revoke
        await _refreshRepo.RevokeAsync(tokenEntity, ipAddress);

        return await GenerateTokensAsync(user, ipAddress);
    }

    public async Task RevokeAsync(string refreshToken, string ipAddress)
    {
        var tokenEntity = await _refreshRepo.GetByTokenAsync(refreshToken);
        if (tokenEntity != null)
        {
            await _refreshRepo.RevokeAsync(tokenEntity, ipAddress);
        }
    }
}
