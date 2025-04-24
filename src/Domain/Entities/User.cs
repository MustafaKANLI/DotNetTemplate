namespace DotNetTemplate.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ICollection<UserContact> Contacts { get; set; } = new List<UserContact>();
}
