namespace Delex_POS.Domain.Entities;

public class PriceListImplementation : BaseAuditableEntity
{
    public int PriceListId { get; set; }
    public int BranchId { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public DateOnly EffectivityDate { get; set; }

}
