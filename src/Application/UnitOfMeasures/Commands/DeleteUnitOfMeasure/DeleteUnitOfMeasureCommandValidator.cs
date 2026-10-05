using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
namespace Delex_POS.Application.UnitOfMeasures.Commands.DeleteUnitOfMeasure;

public class DeleteUnitOfMeasureCommandValidator : AbstractValidator<DeleteUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureQueryRepository _query;
    public DeleteUnitOfMeasureCommandValidator(IUnitOfMeasureQueryRepository query)
    {
        _query = query;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(Exist);
    }
    private async Task<bool> Exist(int id, CancellationToken cancellationToken)
    {
        var entity = await _query.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
