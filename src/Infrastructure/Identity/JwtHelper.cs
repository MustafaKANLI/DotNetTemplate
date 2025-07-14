using DotNetTemplate.Infrastructure.DTOs;
using DotNetTemplate.Infrastructure.Identity.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetTemplate.Infrastructure.Identity;

public class JwtHelper : IJwtHelper
{
    private readonly IConfiguration _config;

    public JwtHelper(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(UserDto user, IEnumerable<ClaimDto> claims)
    {
        var jwtClaims = claims.Select(c => new Claim(c.Type, c.Value)).ToList();
        jwtClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        jwtClaims.Add(new Claim(ClaimTypes.Name, user.Username));

        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"] ?? "60"));


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(jwtClaims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public DateTime GetTokenExpiration(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        return jwtToken.ValidTo.ToLocalTime();
    }
}