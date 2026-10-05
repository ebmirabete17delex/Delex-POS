using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;
namespace Delex_POS.Application.ProductPurchasings.Queries.GetProductPurchasing;

public record GetProductPurchasingQuery(int ProductId) : IRequest<ProductPurchasingDto>;
public class GetProductPurchasingQueryHandler : IRequestHandler<GetProductPurchasingQuery, ProductPurchasingDto>
{
    private readonly IMapper _mapper;
    private readonly IProductPurchasingQueryRepository _query;
    public GetProductPurchasingQueryHandler(IMapper mapper, 
        IProductPurchasingQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<ProductPurchasingDto> Handle(GetProductPurchasingQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByProductIdAsync(request.ProductId, cancellationToken);
        Guard.Against.NotFound(request.ProductId, entity);
        return _mapper.Map<ProductPurchasingDto>(entity);
    }
}
