namespace Delex_POS.Domain.Entities;

public class PriceList : BaseAuditableEntity
{
    public string Description { get; set; } = string.Empty;
    public int PriceListTypeId { get; set; }
    public PriceListStatus Status { get; set; }
}
