using System;
using System.Collections.Generic;
using System.Text;

namespace Delex_POS.Domain.Entities;

public class PriceListItem : BaseAuditableEntity
{
    public int PriceListId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
}
