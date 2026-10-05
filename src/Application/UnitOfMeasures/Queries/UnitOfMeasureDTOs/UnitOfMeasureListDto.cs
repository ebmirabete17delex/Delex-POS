using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.UnitOfMeasures.Queries.UnitOfMeasureDTOs;

public class UnitOfMeasureListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UnitOfMeasure, UnitOfMeasureListDto>();
        }
    }
}
