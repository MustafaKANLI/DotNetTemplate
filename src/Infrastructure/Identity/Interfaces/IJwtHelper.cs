using DotNetTemplate.Infrastructure.DTOs;

namespace DotNetTemplate.Infrastructure.Identity.Interfaces;

public interface IJwtHelper
{
    string GenerateJwtToken(UserDto user, IEnumerable<ClaimDto> claims);
    DateTime GetTokenExpiration(string token);
}