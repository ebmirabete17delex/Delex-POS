using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.PriceLevels.Queries.PriceLevelDTOs;

public class PriceLevelDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Memo { get; set; }
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PriceLevel, PriceLevelDto>();
        }
    }
}
