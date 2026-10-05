using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
namespace Delex_POS.Application.UnitOfMeasures.Commands.CreateUnitOfMeasure;

public class CreateUnitOfMeasureCommandValidator : AbstractValidator<CreateUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureQueryRepository _query;
    public CreateUnitOfMeasureCommandValidator(IUnitOfMeasureQueryRepository query) 
    {
        _query = query;

        RuleFor(v => v.UnitOfMeasureId)
            .NotNull()
            .NotEmpty()
            .MustAsync(UOMIdNotExist);

        RuleFor(v => v.Name)
            .NotNull().NotEmpty();

        RuleFor(v => v.UnitBase)
            .NotEmpty().NotNull();

        RuleFor(v => v.DecimalPlaces)
            .NotEmpty().NotNull();
    }
    private async Task<bool> UOMIdNotExist(string id, CancellationToken cancellationToken)
    {
        var exists = await _query.ExistAsync(e => e.UnitOfMeasureId == id, cancellationToken);
        return !exists;
    }


}
