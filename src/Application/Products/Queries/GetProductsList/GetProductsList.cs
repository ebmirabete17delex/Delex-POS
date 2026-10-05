using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.Products.Queries.ProductDTOs;

namespace Delex_POS.Application.Products.Queries.GetProductsList;

public record GetProductsListQuery : IRequest<List<ProductListDto>>;
public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, List<ProductListDto>>
{
    private readonly IProductQueryRepository _query;
    private readonly IMapper _mapper;
    public GetProductsListQueryHandler(IMapper mapper, IProductQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<ProductListDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<ProductListDto>(_mapper.ConfigurationProvider);
    }
}
