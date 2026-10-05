using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductCategories.Queries.ProductCategoryDTOs;
public class ProductCategoryDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset Created { get; init; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
        }
    }
};
