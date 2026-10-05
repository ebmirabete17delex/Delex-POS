using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;

namespace Delex_POS.Application.CancelReasons.Commands.CreateCancelReason;

public class CreateCancelReasonCommandValidator : AbstractValidator<CreateCancelReasonCommand>
{
    private readonly ICancelReasonQueryRepository _cancelReasonQueryRepository;
    public CreateCancelReasonCommandValidator(ICancelReasonQueryRepository cancelReasonQueryRepository)
    {
        _cancelReasonQueryRepository = cancelReasonQueryRepository;

        RuleFor(v => v.Name)
            .NotEmpty()
            .NotNull()
            .MustAsync(NameNotExists);
    }

    private async Task<bool> NameNotExists(string name, CancellationToken cancellationToken)
    {
        var exists = await _cancelReasonQueryRepository.ExistAsync(e => e.Name == name, cancellationToken);
        return !exists;
    }
}
