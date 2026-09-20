using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.AuthModule;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }

    [NotMapped] 
    public bool IsActive => ExpiresAt > DateTime.UtcNow;
    
    ///////////////////////////////////////////////////////////////
    public User User { get; set; } = null!;
}