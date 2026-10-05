using Delex_POS.Application.Common.Interfaces.Repositories.Generic;

namespace Delex_POS.Application.GenericItems.Commands.DeleteGeneric;

public class DeleteGenericCommandValidator : AbstractValidator<DeleteGenericCommand>
{
    private readonly IGenericQueryRepository _query;
    public DeleteGenericCommandValidator(IGenericQueryRepository query)
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
