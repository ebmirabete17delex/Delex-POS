using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductClasses.Queries.ProductClassDTOs;

public class ProductClassListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductClass, ProductClassListDto>();
        }
    }
}
