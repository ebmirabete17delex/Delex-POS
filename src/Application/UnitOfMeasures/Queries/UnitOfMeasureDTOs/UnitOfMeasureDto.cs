using Delex_POS.Domain.Entities;
namespace Delex_POS.Application.UnitOfMeasures.Queries.UnitOfMeasureDTOs;

public class UnitOfMeasureDto
{
    public string UnitOfMeasureId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string UnitBase { get; set; } = string.Empty;
    public bool IsGroupAsSingleQuantity { get; set; }
    public bool IsAsForQtyWhenSold { get; set; }
    public int DecimalPlaces { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UnitOfMeasure, UnitOfMeasureDto>();
        }
    }
}
