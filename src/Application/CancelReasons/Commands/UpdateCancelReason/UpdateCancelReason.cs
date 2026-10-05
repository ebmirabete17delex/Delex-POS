using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;

namespace Delex_POS.Application.CancelReasons.Commands.UpdateCancelReason;

public record UpdateCancelReasonCommand : IRequest
{
    public int Id { get; set; }
    public string? Memo { get; set; }
}

public class UpdateCancelReasonCommandHandler : IRequestHandler<UpdateCancelReasonCommand>
{
    private readonly ICancelReasonCommandRepository _cancelReasonCommandRepository;
    private readonly ICancelReasonQueryRepository _cancelReasonQueryRepository;

    public UpdateCancelReasonCommandHandler(ICancelReasonCommandRepository cancelReasonCommandRepository, ICancelReasonQueryRepository cancelReasonQueryRepository)
    {
        _cancelReasonCommandRepository = cancelReasonCommandRepository;
        _cancelReasonQueryRepository = cancelReasonQueryRepository;
    }

    public async Task Handle(UpdateCancelReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _cancelReasonQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(request.Memo ?? string.Empty);
        await _cancelReasonCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
