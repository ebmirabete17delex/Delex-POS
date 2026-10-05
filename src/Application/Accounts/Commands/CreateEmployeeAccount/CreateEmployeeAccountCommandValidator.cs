using Delex_POS.Application.Common.Interfaces.Repositories.Account;

namespace Delex_POS.Application.Accounts.Commands.CreateEmployeeAccount;

public class CreateEmployeeAccountCommandValidator : AbstractValidator<CreateEmployeeAccountCommand>
{
    private readonly IAccountCommandRepository _accountCommandRepository;
    public CreateEmployeeAccountCommandValidator(IAccountCommandRepository accountCommandRepository)
    {
        _accountCommandRepository = accountCommandRepository;

        RuleFor(v => v.AccountId)
            .NotEmpty()
            .NotNull();
    }
}
