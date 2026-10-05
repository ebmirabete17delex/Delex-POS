using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Regions.Queries.RegionDTOs;

public class RegionListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Region, RegionListDto>();
        }
    }
}
