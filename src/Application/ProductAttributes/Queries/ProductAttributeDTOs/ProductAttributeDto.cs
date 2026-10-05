using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductAttributes.Queries.ProductAttributeDTOs;
public class ProductAttributeDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductAttribute, ProductAttributeDto>();
        }
    }
};
