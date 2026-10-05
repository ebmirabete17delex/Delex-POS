using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Application.CustomerTypes.Queries.CustomerTypeDTOs;
using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.CustomerTypes.Queries.GetCustomerType;

public record GetCustomerTypeQuery(int Id) : IRequest<CustomerTypeDto>;

public class GetCustomerTypeQueryHandler : IRequestHandler<GetCustomerTypeQuery, CustomerTypeDto>
{
    private readonly IMapper _mapper;
    private readonly ICustomerTypeQueryRepository _query;

    public GetCustomerTypeQueryHandler(IMapper mapper, ICustomerTypeQueryRepository query)
    {
        _mapper = mapper;
        _query = query;
    }

    public async Task<CustomerTypeDto> Handle(GetCustomerTypeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _query.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<CustomerTypeDto>(entity);
    }
}
