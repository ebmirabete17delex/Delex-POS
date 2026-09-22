using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim; 

namespace Delex_POS.Application.AccessClaims.Commands.DeleteAccessClaim;

public record DeleteAccessClaimCommand(int Id) : IRequest;

public class DeleteAccessClaimCommandHandler : IRequestHandler<DeleteAccessClaimCommand>
{
    private readonly IAccessClaimCommandRepository _accessClaimCommandRepository;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;

    public DeleteAccessClaimCommandHandler(IAccessClaimCommandRepository accessClaimCommandRepository, IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _accessClaimCommandRepository = accessClaimCommandRepository;
        _accessClaimQueryRepository = accessClaimQueryRepository;
    }

    public async Task Handle(DeleteAccessClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await _accessClaimCommandRepository.DeleteAsync(request.Id, cancellationToken);
    }
}