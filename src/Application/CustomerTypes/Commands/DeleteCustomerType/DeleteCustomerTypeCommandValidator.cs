using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;

namespace Delex_POS.Application.CustomerTypes.Commands.DeleteCustomerType;

public class DeleteCustomerTypeCommandValidator : AbstractValidator<DeleteCustomerTypeCommand>
{
    private readonly ICustomerTypeQueryRepository _query;
    public DeleteCustomerTypeCommandValidator(ICustomerTypeQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);
    }

    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _query.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
