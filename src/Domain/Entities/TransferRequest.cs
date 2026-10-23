namespace Delex_POS.Domain.Entities;

public class TransferRequest : BaseAuditableEntity
{
    public int BranchFrom { get; set; }
    public int BranchTo { get; set; }
}
