using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
namespace Delex_POS.Application.ProductSales.Queries.GetProductSale;

public record GetProductSaleQuery(int ProductId) : IRequest<ProductSaleDto>;
public class GetProductSaleQueryHandler : IRequestHandler<GetProductSaleQuery, ProductSaleDto>
{
    private readonly IMapper _mapper;
    private readonly IProductSaleQueryRepository _query;
    public GetProductSaleQueryHandler(IMapper mapper, 
        IProductSaleQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<ProductSaleDto> Handle(GetProductSaleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByProductIdAsync(request.ProductId, cancellationToken);
        Guard.Against.NotFound(request.ProductId, entity);
        return _mapper.Map<ProductSaleDto>(entity);
    }
}
