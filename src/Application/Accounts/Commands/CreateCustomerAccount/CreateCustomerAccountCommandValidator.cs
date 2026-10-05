using Delex_POS.Application.Common.Interfaces.Repositories.Account;

namespace Delex_POS.Application.Accounts.Commands.CreateCustomerAccount;

public class CreateCustomerAccountCommandValidator : AbstractValidator<CreateCustomerAccountCommand>
{
    private readonly IAccountCommandRepository _accountCommandRepository;
    public CreateCustomerAccountCommandValidator(IAccountCommandRepository accountCommandRepository)
    {
        _accountCommandRepository = accountCommandRepository;

        RuleFor(v => v.AccountId)
            .NotEmpty()
            .NotNull();
    }

}
