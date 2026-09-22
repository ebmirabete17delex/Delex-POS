using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;

namespace Delex_POS.Application.AccessClaims.Commands.CreateAccessClaim;

public record CreateAccessClaimCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Feature { get; set; } = string.Empty;
    public string BackendUrl { get; set; } = string.Empty;
    public string FrontendUrl { get; set; } = string.Empty;

}

public class CreateAccessClaimCommandHandler : IRequestHandler<CreateAccessClaimCommand, int>
{
    private readonly IAccessClaimCommandRepository _accessClaimCommandRepository;

    public CreateAccessClaimCommandHandler(IAccessClaimCommandRepository accessClaimCommandRepository)
    {
        _accessClaimCommandRepository = accessClaimCommandRepository;
    }

    public async Task<int> Handle(CreateAccessClaimCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.RBAC.AccessClaim(
            name: request.Name,
            feature: request.Feature,
            backendUrl: request.BackendUrl,
            frontendUrl: request.FrontendUrl
        );

        return await _accessClaimCommandRepository.AddAsync(entity,cancellationToken);

    }
}
