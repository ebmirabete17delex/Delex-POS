using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;

namespace Delex_POS.Application.CustomerTypes.Commands.UpdateCustomerType;

public class UpdateCustomerTypeCommandValidator : AbstractValidator<UpdateCustomerTypeCommand>
{
    private readonly ICustomerTypeQueryRepository _query;
    public UpdateCustomerTypeCommandValidator(ICustomerTypeQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull();
    }

    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _query.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
