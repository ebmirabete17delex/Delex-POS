using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Customers.Queries.CustomerDTOs;

namespace Delex_POS.Application.Customers.Queries.GetCustomer;

public record GetCustomerQuery(int Id) : IRequest<CustomerDto>;
public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IMapper _mapper;
    private readonly ICustomerQueryRepository _customerQueryRepository;
    public GetCustomerQueryHandler(IMapper mapper, ICustomerQueryRepository customerQueryRepository)
    {
        _mapper = mapper;
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _customerQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<CustomerDto>(entity);
    }
}
