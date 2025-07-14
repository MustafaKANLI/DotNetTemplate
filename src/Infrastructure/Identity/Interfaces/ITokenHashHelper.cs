namespace DotNetTemplate.Infrastructure.Identity.Interfaces;

public interface ITokenHashHelper
{
    string Hash(string raw);
    bool Verify(string raw, string hash);
}
