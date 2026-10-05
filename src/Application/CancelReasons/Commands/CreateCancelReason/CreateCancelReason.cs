using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;

namespace Delex_POS.Application.CancelReasons.Commands.CreateCancelReason;

public record CreateCancelReasonCommand : IRequest<int>
{
    public string Name { get;  set; } = string.Empty;
    public bool IsActive { get;  set; } = true;
    public bool IsAllowUserToEnterMemo { get;  set; }
    public string? Memo { get;  set; }

}

public class CreateCancelReasonCommandHandler : IRequestHandler<CreateCancelReasonCommand, int>
{
    private readonly ICancelReasonCommandRepository _cancelReasonCommandRepository;

    public CreateCancelReasonCommandHandler(ICancelReasonCommandRepository cancelReasonCommandRepository)
    {
        _cancelReasonCommandRepository = cancelReasonCommandRepository;
    }

    public async Task<int> Handle(CreateCancelReasonCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.CancelReason(
            name: request.Name,
            isActive: request.IsActive,
            isAllowUserToEnterMemo: request.IsAllowUserToEnterMemo,
            memo: request.Memo ?? string.Empty
        );

        return await _cancelReasonCommandRepository.AddAsync(entity, cancellationToken);
    }
}
