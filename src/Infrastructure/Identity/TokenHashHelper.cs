using DotNetTemplate.Infrastructure.Identity.Interfaces;

namespace DotNetTemplate.Infrastructure.Identity;

public class TokenHashHelper : ITokenHashHelper
{
    public string Hash(string raw)
    {
        return BCrypt.Net.BCrypt.HashPassword(raw);
    }

    public bool Verify(string raw, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(raw, hash);
    }
}
