using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.ProductClasses.Queries.ProductClassDTOs;

namespace Delex_POS.Application.ProductClasses.Queries.GetProductClassesList;

public record GetProductClassesListQuery : IRequest<List<ProductClassListDto>>;
public class GetProductClassesListQueryHandler : IRequestHandler<GetProductClassesListQuery, List<ProductClassListDto>>
{
    private readonly IProductClassQueryRepository _query;
    private readonly IMapper _mapper;
    public GetProductClassesListQueryHandler(IMapper mapper, IProductClassQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<ProductClassListDto>> Handle(GetProductClassesListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<ProductClassListDto>(_mapper.ConfigurationProvider);
    }
}
