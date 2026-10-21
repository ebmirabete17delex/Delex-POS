namespace Delex_POS.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string HashedToken { get; set; } = default!;
    public string SessionId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTimeOffset Expires { get; set; }
    public bool Revoked { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
