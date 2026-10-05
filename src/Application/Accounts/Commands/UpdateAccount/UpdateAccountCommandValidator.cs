using Delex_POS.Application.Common.Interfaces.Repositories.Account;

namespace Delex_POS.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    private readonly IAccountQueryRepository _accountQueryRepository;
    public UpdateAccountCommandValidator(IAccountQueryRepository accountQueryRepository)
    {
        _accountQueryRepository = accountQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(AccountExists);

        RuleFor(v => v.AccessLevel)
            .NotEmpty()
            .NotNull();
    }

    private async Task<bool> AccountExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _accountQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
