using Delex_POS.Application.Common.Interfaces.Repositories.Account;

namespace Delex_POS.Application.Accounts.Commands.CreateSupplierAccount;

public class CreateSupplierAccountCommandValidator : AbstractValidator<CreateSupplierAccountCommand>
{
    private readonly IAccountCommandRepository _accountCommandRepository;
    public CreateSupplierAccountCommandValidator(IAccountCommandRepository accountCommandRepository)
    {
        _accountCommandRepository = accountCommandRepository;

        RuleFor(v => v.AccountId)
            .NotEmpty()
            .NotNull();
    }
}
