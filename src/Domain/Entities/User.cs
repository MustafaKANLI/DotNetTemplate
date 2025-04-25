namespace DotNetTemplate.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public byte[]? PWSalt { get; set; }
    public byte[]? PWHash { get; set; }
    public bool IsActive { get; set; }
    public bool IsLocked { get; set; }
    public ICollection<UserContact> Contacts { get; set; } = new List<UserContact>();
}
