using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Products.Queries.ProductDTOs;

namespace Delex_POS.Application.Products.Queries.GetProduct;

public record GetProductQuery(int Id) : IRequest<ProductDto>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IMapper _mapper;
    private readonly IProductQueryRepository _productQueryRepository;

    public GetProductQueryHandler(
        IMapper mapper, 
        IProductQueryRepository productQueryRepository)
    {
        _mapper = mapper;
        _productQueryRepository = productQueryRepository;
    }

    public async Task<ProductDto> Handle(
        GetProductQuery request, 
        CancellationToken cancellationToken)
    {
        var entity = await _productQueryRepository
            .GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<ProductDto>(entity);
    }
}
