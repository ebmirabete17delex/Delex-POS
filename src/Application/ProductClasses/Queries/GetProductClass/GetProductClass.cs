using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.ProductClasses.Queries.ProductClassDTOs;

namespace Delex_POS.Application.ProductClasses.Queries.GetProductClass;

public record GetProductClassQuery() : IRequest<ProductClassDto>
{
    public int Id { get; init; }
};
public class GetProductClassQueryHandler : IRequestHandler<GetProductClassQuery, ProductClassDto>
{
    private readonly IProductClassQueryRepository _productClassQueryRepository;
    private readonly IMapper _mapper;

    public GetProductClassQueryHandler(IProductClassQueryRepository productClassQueryRepository, IMapper mapper)
    {
        _productClassQueryRepository = productClassQueryRepository;
        _mapper = mapper;
    }

    public async Task<ProductClassDto> Handle(GetProductClassQuery request, CancellationToken cancellationToken)
    {
        var entity = await _productClassQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<ProductClassDto>(entity);
    }
}
