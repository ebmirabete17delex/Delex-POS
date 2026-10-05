using Delex_POS.Application.Common.Interfaces.Repositories.Account;

namespace Delex_POS.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    private readonly IAccountQueryRepository _accountQueryRepository;
    public DeleteAccountCommandValidator(IAccountQueryRepository accountQueryRepository)
    {
        _accountQueryRepository = accountQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccountExists);
    }

    private async Task<bool> AccountExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _accountQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
