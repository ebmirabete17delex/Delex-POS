using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Application.Accounts.Queries.AccountDTOs;

namespace Delex_POS.Application.Accounts.Queries.GetAccount;

public record GetAccountQuery(int Id) : IRequest<AccountDto>;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
{
    private readonly IMapper _mapper;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public GetAccountQueryHandler(IMapper mapper, IAccountQueryRepository accountQueryRepository)
    {
        _mapper = mapper;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var entity = await _accountQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return _mapper.Map<AccountDto>(entity);
    }
}
