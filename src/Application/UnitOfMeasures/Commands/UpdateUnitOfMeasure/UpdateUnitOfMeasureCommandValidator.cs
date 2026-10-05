using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;

namespace Delex_POS.Application.UnitOfMeasures.Commands.UpdateUnitOfMeasure;

public class UpdateUnitOfMeasureCommandValidator : AbstractValidator<UpdateUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureQueryRepository _unitOfMeasureQueryRepository;
    public UpdateUnitOfMeasureCommandValidator(
        IUnitOfMeasureQueryRepository unitOfMeasureQueryRepository
        )
    {
        _unitOfMeasureQueryRepository = unitOfMeasureQueryRepository;

        RuleFor(v => v.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(UOMExist);
    }

    private async Task<bool> UOMExist(int id, CancellationToken cancellationToken)
    {
        return await _unitOfMeasureQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
    }

}
