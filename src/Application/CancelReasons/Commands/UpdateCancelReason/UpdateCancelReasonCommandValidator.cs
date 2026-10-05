using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;

namespace Delex_POS.Application.CancelReasons.Commands.UpdateCancelReason;

public class UpdateCancelReasonCommandValidator : AbstractValidator<UpdateCancelReasonCommand>
{
    private readonly ICancelReasonQueryRepository _cancelReasonQueryRepository;
    public UpdateCancelReasonCommandValidator(ICancelReasonQueryRepository cancelReasonQueryRepository)
    {
        _cancelReasonQueryRepository = cancelReasonQueryRepository;

        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(CancelReasonExists);
    }

    private async Task<bool> CancelReasonExists(int id, CancellationToken cancellationToken)
    {
        var entity = await _cancelReasonQueryRepository.ExistAsync(e => e.Id == id, cancellationToken);
        return entity;
    }
}
