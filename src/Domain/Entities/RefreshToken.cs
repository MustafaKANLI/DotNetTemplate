namespace DotNetTemplate.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string TokenHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string CreatedByIp { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByTokenId { get; set; }
    
    // Navigation property
    public Guid UserId { get; set; }
    public User User { get; set; }
}