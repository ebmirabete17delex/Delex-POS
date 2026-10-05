using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Regions.Queries.RegionDTOs;
public class RegionDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Region, RegionDto>();
        }
    }
};
