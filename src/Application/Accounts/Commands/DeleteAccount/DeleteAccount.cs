using Delex_POS.Application.Common.Interfaces.Repositories.Account;

namespace Delex_POS.Application.Accounts.Commands.DeleteAccount;

public record DeleteAccountCommand(int Id) : IRequest;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountCommandRepository _accountCommandRepository;
    private readonly IAccountQueryRepository _accountQueryRepository;

    public DeleteAccountCommandHandler(IAccountCommandRepository accountCommandRepository, IAccountQueryRepository accountQueryRepository)
    {
        _accountCommandRepository = accountCommandRepository;
        _accountQueryRepository = accountQueryRepository;
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accountQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _accountCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
