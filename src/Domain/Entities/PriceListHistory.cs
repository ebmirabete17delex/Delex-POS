namespace Delex_POS.Domain.Entities;

public class PriceListHistory : BaseAuditableEntity
{
    public int PriceListId { get; set; }
    public PriceListStatus Status { get; set; }
    public DateTime StatusDate { get; set; }
    public int StatusBy { get; set; }
    public string Remarks { get; set; } = string.Empty;
}
