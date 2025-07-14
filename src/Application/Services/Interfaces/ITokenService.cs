using DotNetTemplate.Application.Common;
using DotNetTemplate.Domain.Entities;
using DotNetTemplate.Infrastructure.DTOs;

namespace DotNetTemplate.Application.Services.Interfaces;

public interface ITokenService
{
    Task<(string accessToken, string refreshToken)> GenerateTokensAsync(UserDto user, string ipAddress);
    Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken, string ipAddress);
    Task RevokeAsync(string refreshToken, string ipAddress);
}
