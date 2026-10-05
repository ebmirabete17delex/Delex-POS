using Delex_POS.Application.Common.Interfaces.Repositories.Generic;

namespace Delex_POS.Application.GenericItems.Commands.CreateGeneric;

public class CreateGenericCommandValidator : AbstractValidator<CreateGenericCommand>
{
    private readonly IGenericQueryRepository _query;

    public CreateGenericCommandValidator(IGenericQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Code)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .MustAsync(CodeNotExists);

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);
    }

    private async Task<bool> CodeNotExists(string code, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.Code == code, cancellationToken);
        return !exists;
    }
}
