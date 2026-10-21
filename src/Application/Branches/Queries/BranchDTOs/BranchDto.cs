using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Branches.Queries.BranchDTOs;
public class BranchDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int WarehouseId { get; init; }
    public string? Warehouse { get; init; }
    public string RegionCode { get; init; } = string.Empty;
    public string? Region { get; init; }
    public string ProvinceCode { get; init; } = string.Empty;
    public string? Province { get; init; }
    public string MunicipalityCode { get; init; } = string.Empty;
    public string? Municipality { get; init; }
    public string BarangayCode { get; init; } = string.Empty;
    public string? Barangay { get; init; }
    public string? Address1 { get; init; } = string.Empty;
    public string? Address2 { get; init; } = string.Empty;
    public string? Phone { get; init; } = string.Empty;
    public string? Fax { get; init; } = string.Empty;
    public string? Email { get; init; } = string.Empty;
    public string? TIN { get; init; } = string.Empty;
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Branch, BranchDto>();
        }
    }
};
