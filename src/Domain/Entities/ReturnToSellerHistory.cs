namespace Delex_POS.Domain.Entities;

public class ReturnToSellerHistory : BaseAuditableEntity
{
    public int ReturnToSellerId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public int UserId { get; set; }
}
