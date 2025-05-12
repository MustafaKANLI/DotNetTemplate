namespace DotNetTemplate.Infrastructure.DTOs;

public class LoginRequestDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public string? RefreshToken { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public UserDto User { get; set; }
}