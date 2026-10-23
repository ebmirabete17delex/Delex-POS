namespace Delex_POS.Domain.Entities;

public class TransferRequestHistory : BaseAuditableEntity
{
    public int TransferRequestId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public int UserId { get; set; }
    public int CancelReasonId { get; set; }
    public string Remarks { get; set; } = string.Empty;
}
