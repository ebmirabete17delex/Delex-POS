using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;

namespace Delex_POS.Application.CustomerTypes.Commands.CreateCustomerType;

public class CreateCustomerTypeCommandValidator : AbstractValidator<CreateCustomerTypeCommand>
{
    private readonly ICustomerTypeQueryRepository _customerTypeQueryRepository;

    public CreateCustomerTypeCommandValidator(ICustomerTypeQueryRepository customerTypeQueryRepository)
    {
        _customerTypeQueryRepository = customerTypeQueryRepository;

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200)
            .MustAsync(NameNotExists);
    }

    private async Task<bool> NameNotExists(string name, CancellationToken cancellationToken)
    {
        var exists = await _customerTypeQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }
}
