using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;

namespace Delex_POS.Application.CancelReasons.Commands.DeleteCancelReason;

public record DeleteCancelReasonCommand(int Id) : IRequest;

public class DeleteCancelReasonCommandHandler : IRequestHandler<DeleteCancelReasonCommand>
{
    private readonly ICancelReasonCommandRepository _cancelReasonCommandRepository;
    private readonly ICancelReasonQueryRepository _cancelReasonQueryRepository;

    public DeleteCancelReasonCommandHandler(ICancelReasonCommandRepository cancelReasonCommandRepository, ICancelReasonQueryRepository cancelReasonQueryRepository)
    {
        _cancelReasonCommandRepository = cancelReasonCommandRepository;
        _cancelReasonQueryRepository = cancelReasonQueryRepository;
    }

    public async Task Handle(DeleteCancelReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _cancelReasonQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _cancelReasonCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
