namespace DotNetTemplate.Application.DTOs;

public class UserContactDto
{
    public Guid Id { get; set; }
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Country { get; set; } = null!;
}

public class CreateUserContactDto
{
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Country { get; set; } = null!;
    public Guid UserId { get; set; }
}
