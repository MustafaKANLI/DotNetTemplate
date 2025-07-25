using DotNetTemplate.Domain.Entities;

namespace DotNetTemplate.Infrastructure.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public List<UserContactDto> Contacts { get; set; } = new();
    public Role Role { get; set; } = null!;
}

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid RoleId { get; set; }

}


