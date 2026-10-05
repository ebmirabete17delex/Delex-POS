using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;

namespace Delex_POS.Application.ProductClasses.Queries.ProductClassDTOs;
public class ProductClassDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string? Memo { get; set; }
    public int ProductTypeId { get; set; }
    public TaxType TaxTypeId { get; set; }
    public int UnitOfMeasurementId { get; set; }
    public int GenericNameId { get; set; }
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductClass, ProductClassDto>();
        }
    }
};
