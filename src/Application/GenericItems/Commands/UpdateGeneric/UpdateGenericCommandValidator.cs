using Delex_POS.Application.Common.Interfaces.Repositories.Generic;

namespace Delex_POS.Application.GenericItems.Commands.UpdateGeneric;

public class UpdateGenericCommandValidator : AbstractValidator<UpdateGenericCommand>
{
    private readonly IGenericQueryRepository _query;
    public UpdateGenericCommandValidator(IGenericQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exists);

        RuleFor(v => v.Code)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);
    }

    private async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await _query.ExistAsync(e => e.Id == id, cancellationToken);
    }
}
