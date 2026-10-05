using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;

public class WarehouseDto
{
    public int Id { get; init; }
    public string WarehouseId { get; init; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
    public bool IsActive { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Warehouse, WarehouseDto>();
        }
    }

}
