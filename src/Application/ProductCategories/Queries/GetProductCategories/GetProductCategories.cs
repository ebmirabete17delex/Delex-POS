using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.ProductCategories.Queries.ProductCategoryDTOs;

namespace Delex_POS.Application.ProductCategories.Queries.GetProductCategories;

public record GetProductCategoriesQuery : IRequest<List<ProductCategoryDto>>;
public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategoryDto>>
{
    private readonly IProductCategoryQueryRepository _query;
    private readonly IMapper _mapper;
    public GetProductCategoriesQueryHandler(IMapper mapper, IProductCategoryQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<ProductCategoryDto>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<ProductCategoryDto>(_mapper.ConfigurationProvider);
    }
}
