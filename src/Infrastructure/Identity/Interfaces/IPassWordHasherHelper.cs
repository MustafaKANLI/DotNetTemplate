namespace DotNetTemplate.Infrastructure.Identity.Interfaces;

public interface IPasswordHasherHelper
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
