using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Products.Queries.ProductDTOs;
public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public int ProductClassId { get; set; }
    public int ProductTypeId { get; set; }
    public int UnitOfMeasureId { get; set; }
    public string? Memo { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset Created { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
};
