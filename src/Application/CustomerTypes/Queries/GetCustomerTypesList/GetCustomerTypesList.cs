using Delex_POS .Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Application.Common.Mappings;
using Delex_POS.Application.CustomerTypes.Queries.CustomerTypeDTOs;

namespace Delex_POS.Application.CustomerTypes.Queries.GetCustomerTypesList;

public record GetCustomerTypesListQuery : IRequest<List<CustomerTypeDto>>;
public class GetCustomerTypesListQueryHandler : IRequestHandler<GetCustomerTypesListQuery, List<CustomerTypeDto>>
{
    private readonly ICustomerTypeQueryRepository _query;
    private readonly IMapper _mapper;
    public GetCustomerTypesListQueryHandler(IMapper mapper, ICustomerTypeQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }
    public async Task<List<CustomerTypeDto>> Handle(GetCustomerTypesListQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAll().ProjectToListAsync<CustomerTypeDto>(_mapper.ConfigurationProvider);
    }
}


