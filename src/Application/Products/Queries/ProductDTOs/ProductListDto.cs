using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Products.Queries.ProductDTOs;

public class ProductListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductListDto>();
        }
    }
}
