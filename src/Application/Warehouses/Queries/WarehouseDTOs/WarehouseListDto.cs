using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Warehouses.Queries.WarehouseDTOs;

public class WarehouseListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Warehouse, WarehouseListDto>();
        }
    }
}
