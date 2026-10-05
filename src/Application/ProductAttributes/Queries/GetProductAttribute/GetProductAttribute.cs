using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;
using Delex_POS.Application.ProductAttributes.Queries.ProductAttributeDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.ProductAttributes.Queries.GetProductAttribute;

public record GetProductAttributeQuery(int Id) : IRequest<ProductAttributeDto>;

public class GetProductAttributeQueryHandler : IRequestHandler<GetProductAttributeQuery, ProductAttributeDto>
{
    private readonly IMapper _mapper;
    private readonly IProductAttributeQueryRepository _query;

    public GetProductAttributeQueryHandler(IMapper mapper, IProductAttributeQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<ProductAttributeDto> Handle(GetProductAttributeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<ProductAttributeDto>(entity);
    }
}
