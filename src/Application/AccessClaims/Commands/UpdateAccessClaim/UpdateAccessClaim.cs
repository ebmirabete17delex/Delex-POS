using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;

namespace Delex_POS.Application.AccessClaims.Commands.UpdateAccessClaim;

public record UpdateAccessClaimCommand : IRequest
{
    public int Id { get; set; }
    public string Feature { get; set; } = string.Empty;

    public string BackendUrl { get; set; } = string.Empty;

    public string FrontendUrl { get; set; } = string.Empty;
}

public class UpdateAccessClaimCommandHandler : IRequestHandler<UpdateAccessClaimCommand>
{
    private readonly IAccessClaimCommandRepository _accessClaimCommandRepository;
    private readonly IAccessClaimQueryRepository _accessClaimQueryRepository;

    public UpdateAccessClaimCommandHandler(IAccessClaimCommandRepository accessClaimCommandRepository, IAccessClaimQueryRepository accessClaimQueryRepository)
    {
        _accessClaimCommandRepository = accessClaimCommandRepository;
        _accessClaimQueryRepository = accessClaimQueryRepository;
    }

    public async Task Handle(UpdateAccessClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = await _accessClaimQueryRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        entity.Update(
            request.Feature,
            request.BackendUrl,
            request.FrontendUrl
        );
        await _accessClaimCommandRepository.UpdateAsync(entity, cancellationToken);
    }
}
