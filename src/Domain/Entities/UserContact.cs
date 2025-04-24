namespace DotNetTemplate.Domain.Entities;

public class UserContact
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Country { get; set; } = null!;
    public User? User { get; set; }
}
